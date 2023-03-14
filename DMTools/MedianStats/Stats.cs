using NLog;
using System;

namespace DMTools
{
	public class Stats
	{
		public struct TagStat
		{
			public short wSubIndex;

			public short wStatIndex;

			public int dwStatValue;
		}

		private static Logger logger = LogManager.GetCurrentClassLogger();

		private const int numStats = 1024;

		public static int[,] cache = new int[2, 1024];

		public IntPtr g_hD2Client
		{
			get
			{
				return MainWindow.mainInstance.g_hD2Client;
			}
		}

		public IntPtr g_ahD2Handle
		{
			get
			{
				return MainWindow.mainInstance.g_ahD2Handle;
			}
		}

		public IntPtr g_hD2Common
		{
			get
			{
				return MainWindow.mainInstance.g_hD2Common;
			}
		}

		public IntPtr g_pD2sgpt
		{
			get
			{
				return MainWindow.mainInstance.g_pD2sgpt;
			}
		}

		public void UpdateCache()
		{
			for (int i = 0; i < 1024; i++)
			{
				Stats.cache[0, i] = 0;
				Stats.cache[1, i] = 0;
			}
			if (MainWindow.mainInstance.IsIngame())
			{
				//this.UpdateStatValueMem(0);
				this.UpdateStatValueMem(1);
		
			}
		}

		public IntPtr GetUnitToRead()
		{
			bool readMercenary = false;
			return this.g_hD2Client + (readMercenary ? 1091596 : 1162236);
		}
   

        public void UpdateStatValueMem(int iVector)
		{
			if (iVector != 0 && iVector != 1)
			{
				Stats.logger.Debug("UpdateStatValueMem: Invalid iVector value.");
			}
			IntPtr unitToRead = this.GetUnitToRead();
			int[] array = new int[]
			{
				0,
				92,
				(iVector + 1) * 36
			};
			IntPtr address = NomadMemory.MemoryPointerRead(unitToRead, this.g_ahD2Handle, array, "dword");
			array[2] += 4;
			int num = (int)((ushort)((int)NomadMemory.MemoryPointerRead(unitToRead, this.g_ahD2Handle, array, "word")) - 1);
			if (num == -1)
			{
				return;
			}
			Stats.TagStat[] array2 = NomadMemory.ReadProcessMemoryStructArray<Stats.TagStat>(this.g_ahD2Handle, address, num);
			for (int i = 0; i < num; i++)
			{
				int wStatIndex = (int)array2[i].wStatIndex;
				if (wStatIndex < 1024)
				{
					int dwStatValue = array2[i].dwStatValue;
					int num2 = wStatIndex;
					if (num2 >= 6 && num2 <= 11)
					{
						Stats.cache[iVector, wStatIndex] += dwStatValue / 256;
					}
					else
					{
						Stats.cache[iVector, wStatIndex] += dwStatValue;
					}
				}
			}
		}

		public IntPtr GetUnitWeapon(IntPtr pUnit)
		{
			IntPtr expr_1D = (IntPtr)NomadMemory.MemoryRead(pUnit + 96, this.g_ahD2Handle, "dword");
			IntPtr intPtr = (IntPtr)NomadMemory.MemoryRead(expr_1D + 12, this.g_ahD2Handle, "dword");
			int num = NomadMemory.MemoryRead(expr_1D + 28, this.g_ahD2Handle, "dword");
			IntPtr arg_58_0 = IntPtr.Zero;
			IntPtr result = IntPtr.Zero;
			while (intPtr != (IntPtr)0)
			{
				if (num == NomadMemory.MemoryRead(intPtr + 12, this.g_ahD2Handle, "dword"))
				{
					result = intPtr;
					break;
				}
				intPtr = (IntPtr)NomadMemory.MemoryRead((IntPtr)NomadMemory.MemoryRead(intPtr + 20, this.g_ahD2Handle, "dword") + 100, this.g_ahD2Handle, "dword");
			}
			return result;
		}

		public void CalculateWeaponDamage()
		{
			IntPtr pUnit = (IntPtr)NomadMemory.MemoryRead(this.GetUnitToRead(), this.g_ahD2Handle, "dword");
			IntPtr unitWeapon = this.GetUnitWeapon(pUnit);
			if (unitWeapon == IntPtr.Zero)
			{
				return;
			}
			int num = NomadMemory.MemoryRead(unitWeapon + 4, this.g_ahD2Handle, "dword");
			IntPtr pointer = (IntPtr)NomadMemory.MemoryRead(this.g_hD2Common + 654232, this.g_ahD2Handle, "dword") + 424 * num;
			int num2 = NomadMemory.MemoryRead(pointer + 262, this.g_ahD2Handle, "word");
			int num3 = NomadMemory.MemoryRead(pointer + 264, this.g_ahD2Handle, "word");
			bool flag = NomadMemory.MemoryRead(pointer + 284, this.g_ahD2Handle, "byte") != 0;
			bool flag2 = !flag || NomadMemory.MemoryRead(pointer + 317, this.g_ahD2Handle, "byte") != 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			if (flag)
			{
				num5 = Stats.GetStatValue(23);
				num7 = Stats.GetStatValue(24);
			}
			if (flag2)
			{
				num4 = Stats.GetStatValue(21);
				num6 = Stats.GetStatValue(22);
				if (!flag)
				{
					num5 = Stats.GetStatValue(159);
					num7 = Stats.GetStatValue(160);
				}
			}
			if (num6 < num4)
			{
				num6 = num4 + 1;
			}
			if (num7 < num5)
			{
				num7 = num5 + 1;
			}
			int num8 = (Stats.GetStatValue(0, 1) * num2 + Stats.GetStatValue(2, 1) * num3) / 100 - 1;
			int num9 = Stats.GetStatValue(25) + Stats.GetStatValue(343);
			int num10 = 1 + num9 / 100 + num8 / 100;
			int[] array = new int[]
			{
				num4,
				num6,
				num5,
				num7
			};
			for (int i = 0; i <= 3; i++)
			{
				Stats.cache[1, 21 + i] = array[i] * num10;
			}
		}

		public void FixStats()
		{
			for (int i = 67; i <= 69; i++)
			{
				Stats.cache[1, i] = 0;
			}
			Stats.cache[1, 343] = 0;
			IntPtr pointer = (IntPtr)NomadMemory.MemoryRead(this.g_pD2sgpt + 2968, this.g_ahD2Handle, "dword");
			IntPtr pointer2 = (IntPtr)NomadMemory.MemoryRead(this.g_pD2sgpt + 3064, this.g_ahD2Handle, "dword");
			IntPtr pointer3 = (IntPtr)NomadMemory.MemoryRead(this.g_hD2Common + 654232, this.g_ahD2Handle, "dword");
			IntPtr unitToRead = this.GetUnitToRead();
			IntPtr pUnit = (IntPtr)NomadMemory.MemoryRead(unitToRead, this.g_ahD2Handle, "dword");
			int[] av_Offset = new int[]
			{
				0,
				92,
				60
			};
			IntPtr intPtr = NomadMemory.MemoryPointerRead(unitToRead, this.g_ahD2Handle, av_Offset, "dword");
			while (intPtr != (IntPtr)0)
			{
				int num = NomadMemory.MemoryRead(intPtr + 8, this.g_ahD2Handle, "dword");
				IntPtr pointer4 = (IntPtr)NomadMemory.MemoryRead(intPtr + 36, this.g_ahD2Handle, "dword");
				int num2 = NomadMemory.MemoryRead(intPtr + 40, this.g_ahD2Handle, "word");
				intPtr = (IntPtr)NomadMemory.MemoryRead(intPtr + 44, this.g_ahD2Handle, "dword");
				int num3 = 0;
				for (int j = 0; j < num2; j++)
				{
					int num4 = NomadMemory.MemoryRead(pointer4 + j * 8 + 2, this.g_ahD2Handle, "word");
					int num5 = NomadMemory.MemoryRead(pointer4 + j * 8 + 4, this.g_ahD2Handle, "dword");
					if (num4 == 350 && num5 != 511)
					{
						num3 = num5;
					}
					if (num == 4 && num4 == 67)
					{
						Stats.cache[1, num4] += num5;
					}
				}
				if (num != 4)
				{
					if (NomadMemory.MemoryRead(intPtr + 20, this.g_ahD2Handle, "dword") == 195)
					{
						num3 = 687;
					}
					bool[] array = new bool[3];
					if (num3 != 0)
					{
						IntPtr pointer5 = pointer + 572 * num3;
						for (int k = 0; k <= 4; k++)
						{
							int num4 = NomadMemory.MemoryRead(pointer5 + 152 + k * 2, this.g_ahD2Handle, "word");
							int num6 = num4;
							if (num6 >= 67 && num6 <= 69)
							{
								array[num4 - 67] = true;
							}
						}
						for (int l = 0; l <= 5; l++)
						{
							int num4 = NomadMemory.MemoryRead(pointer5 + 84 + l * 2, this.g_ahD2Handle, "word");
							int num7 = num4;
							if (num7 >= 67 && num7 <= 69)
							{
								array[num4 - 67] = true;
							}
						}
					}
					for (int m = 0; m < num2; m++)
					{
						int num4 = NomadMemory.MemoryRead(pointer4 + m * 8 + 2, this.g_ahD2Handle, "word");
						int num5 = NomadMemory.MemoryRead(pointer4 + m * 8 + 4, this.g_ahD2Handle, "dword");
						int num8 = num4;
						if (num8 >= 67 && num8 <= 69)
						{
							if (num3 == 0 || array[num4 - 67])
							{
								Stats.cache[1, num4] += num5;
							}
						}
						else if (num4 == 343)
						{
							int num9 = NomadMemory.MemoryRead(pointer4 + m * 8 + 0, this.g_ahD2Handle, "word");
							IntPtr unitWeapon = this.GetUnitWeapon(pUnit);
							if (!(unitWeapon == IntPtr.Zero) && num9 != 0)
							{
								int num10 = NomadMemory.MemoryRead(unitWeapon + 4, this.g_ahD2Handle, "dword");
								int num11 = NomadMemory.MemoryRead(pointer3 + 424 * num10 + 286, this.g_ahD2Handle, "word");
								bool flag = false;
								int[] array2 = new int[256];
								array2[0] = 1;
								array2[0] = num11;
								for (int n = 1; n <= array2[0]; n++)
								{
									if (array2[n] == num9)
									{
										flag = true;
										break;
									}
									for (int num12 = 0; num12 <= 1; num12++)
									{
										int num13 = NomadMemory.MemoryRead(pointer2 + 228 * array2[n] + 4 + num12 * 2, this.g_ahD2Handle, "word");
										if (num13 != 0)
										{
											array2[0]++;
											array2[array2[0]] = num13;
										}
									}
								}
								if (flag)
								{
									Stats.cache[1, 343] += num5;
								}
							}
						}
					}
				}
			}
		}

		public void FixVeteranToken()
		{
			Stats.cache[1, 219] = 0;
			IntPtr unitToRead = this.GetUnitToRead();
			int[] av_Offset = new int[]
			{
				0,
				96,
				12
			};
			IntPtr intPtr = NomadMemory.MemoryPointerRead(unitToRead, this.g_ahD2Handle, av_Offset, "dword");
			while (intPtr != IntPtr.Zero)
			{
				IntPtr pointer = (IntPtr)NomadMemory.MemoryRead(intPtr + 20, this.g_ahD2Handle, "dword");
				IntPtr intPtr2 = (IntPtr)NomadMemory.MemoryRead(intPtr + 92, this.g_ahD2Handle, "dword");
				intPtr = (IntPtr)NomadMemory.MemoryRead(pointer + 100, this.g_ahD2Handle, "dword");
				if (!(intPtr2 == IntPtr.Zero))
				{
					IntPtr intPtr3 = (IntPtr)NomadMemory.MemoryRead(intPtr2 + 72, this.g_ahD2Handle, "dword");
					if (!(intPtr3 == IntPtr.Zero))
					{
						int num = NomadMemory.MemoryRead(intPtr2 + 76, this.g_ahD2Handle, "word");
						int num2 = 0;
						for (int i = 0; i < num; i++)
						{
							int num3 = NomadMemory.MemoryRead(intPtr3 + i * 8 + 2, this.g_ahD2Handle, "word");
							if (num3 == 83 || num3 == 85 || num3 == 219)
							{
								num2++;
							}
						}
						if (num2 == 3)
						{
							Stats.cache[1, 219] = 1;
							return;
						}
					}
				}
			}
		}

		public static int GetStatValue(int iStatID)
		{
			int iVector = (iStatID < 4) ? 0 : 1;
			return Stats.GetStatValue(iStatID, iVector);
		}

		public static int GetStatValue(int iStatID, int iVector)
		{
			return Stats.cache[iVector, iStatID];
		}
	}
}
