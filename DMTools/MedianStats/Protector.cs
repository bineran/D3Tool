using DMTools.Properties;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace DMTools
{
	internal class Protector
	{
		private DateTime lastPrintTime;

		private DateTime lastPingTime;

		private DateTime lastHealingTime;

		private int lastPing;

		private int pingTimeoutCount;

		private Stats stats = new Stats();

		private Random random = new Random();

		public static void ExitGame()
		{

		}



		private int GetItemClass(IntPtr pItem)
		{
			return NomadMemory.MemoryRead(pItem + 4, MainWindow.mainInstance.g_ahD2Handle, "dword");
		}

		private bool IsItem(IntPtr pItem)
		{
			return NomadMemory.MemoryRead(pItem + 0, MainWindow.mainInstance.g_ahD2Handle, "dword") == 4;
		}

		private IntPtr GetItemData(IntPtr pItem)
		{
			return (IntPtr)NomadMemory.MemoryRead(pItem + 20, MainWindow.mainInstance.g_ahD2Handle, "dword");
		}

		private IntPtr GetNextpItem(IntPtr pItemData)
		{
			return (IntPtr)NomadMemory.MemoryRead(pItemData + 100, MainWindow.mainInstance.g_ahD2Handle, "dword");
		}

		private NODEPAGE GetNodePage(IntPtr pItemData)
		{
			int num = NomadMemory.MemoryRead(pItemData + 105, MainWindow.mainInstance.g_ahD2Handle, "dword");
			if (num - 1 <= 2)
			{
				return (NODEPAGE)num;
			}
			return NODEPAGE.ERROR;
		}

		private bool IsTown()
		{
			int num = NomadMemory.MemoryRead((IntPtr)NomadMemory.MemoryRead((IntPtr)NomadMemory.MemoryRead((IntPtr)NomadMemory.MemoryRead((IntPtr)NomadMemory.MemoryRead((IntPtr)NomadMemory.MemoryRead(MainWindow.mainInstance.g_hD2Client + 1162236, MainWindow.mainInstance.g_ahD2Handle, "dword") + 28, MainWindow.mainInstance.g_ahD2Handle, "dword") + 16, MainWindow.mainInstance.g_ahD2Handle, "dword") + 16, MainWindow.mainInstance.g_ahD2Handle, "dword") + 88, MainWindow.mainInstance.g_ahD2Handle, "dword") + 464, MainWindow.mainInstance.g_ahD2Handle, "dword");
			if (num <= 103)
			{
				if (num <= 40)
				{
					if (num > 1 && num != 40)
					{
						return false;
					}
				}
				else if (num != 75 && num != 103)
				{
					return false;
				}
			}
			else if (num <= 137)
			{
				if (num != 109 && num != 137)
				{
					return false;
				}
			}
			else if (num != 142 && num != 241)
			{
				return false;
			}
			return true;
		}
		public  Tuple<int, int> GetLifeStatu()
		{
    //        NomadMemory.MemoryPointerRead(MainWindow.mainInstance.g_hD2Client + 1162236, MainWindow.mainInstance.g_ahD2Handle, new int[]
    //        {
    //            0,
    //            92,
    //            72
    //        }, "dword");
    //        Marshal.SizeOf(typeof(Stats.TagStat));
    //        bool flag = this.IsTown();
    //        int num = NomadMemory.MemoryRead((IntPtr)NomadMemory.MemoryRead(
				//(IntPtr)NomadMemory.MemoryRead(
				//	(IntPtr)NomadMemory.MemoryRead(
				//		(IntPtr)NomadMemory.MemoryRead(
				//			(IntPtr)NomadMemory.MemoryRead(
				//				MainWindow.mainInstance.g_hD2Client + 1162236,
				//MainWindow.mainInstance.g_ahD2Handle, "dword") + 28,
				//MainWindow.mainInstance.g_ahD2Handle, "dword") + 16, 
				//MainWindow.mainInstance.g_ahD2Handle, "dword") + 16, 
				//MainWindow.mainInstance.g_ahD2Handle, "dword") + 88, 
				//MainWindow.mainInstance.g_ahD2Handle, "dword") + 464, 
				//MainWindow.mainInstance.g_ahD2Handle, "dword");
            this.stats.UpdateCache();
            int num2 = Stats.cache[1, 6];
            int num3 = Stats.cache[1, 7];
			return new Tuple<int,int>(num2,num3);
        }
		public void Do()
		{
			NomadMemory.MemoryPointerRead(MainWindow.mainInstance.g_hD2Client + 1162236, MainWindow.mainInstance.g_ahD2Handle, new int[]
			{
				0,
				92,
				72
			}, "dword");
			Marshal.SizeOf(typeof(Stats.TagStat));
			bool flag = this.IsTown();
			int num = NomadMemory.MemoryRead((IntPtr)NomadMemory.MemoryRead((IntPtr)NomadMemory.MemoryRead((IntPtr)NomadMemory.MemoryRead((IntPtr)NomadMemory.MemoryRead((IntPtr)NomadMemory.MemoryRead(MainWindow.mainInstance.g_hD2Client + 1162236, MainWindow.mainInstance.g_ahD2Handle, "dword") + 28, MainWindow.mainInstance.g_ahD2Handle, "dword") + 16, MainWindow.mainInstance.g_ahD2Handle, "dword") + 16, MainWindow.mainInstance.g_ahD2Handle, "dword") + 88, MainWindow.mainInstance.g_ahD2Handle, "dword") + 464, MainWindow.mainInstance.g_ahD2Handle, "dword");
			this.stats.UpdateCache();
			int num2 = Stats.cache[1, 6];
			int num3 = Stats.cache[1, 7];
			if (Settings.Default.lifeProtect && !flag && (double)(100 * num2 / ((num3 <= 0) ? 1 : num3)) < Settings.Default.lifeProtectValue)
			{
				Protector.ExitGame();
				return;
			}
			if (Settings.Default.pingProtect && !flag && (DateTime.Now - this.lastPingTime).TotalSeconds > 1.0)
			{
				this.lastPingTime = DateTime.Now;
				this.lastPing = NomadMemory.MemoryRead(MainWindow.mainInstance.g_hD2Client + 1153028, MainWindow.mainInstance.g_ahD2Handle, "dword");
				if ((double)this.lastPing < Settings.Default.pingProtectValue)
				{
					this.pingTimeoutCount = 0;
				}
				else
				{
					this.pingTimeoutCount++;
				}
				int num4 = 5;
				int.TryParse(Settings.Default.pingProtectCount, out num4);
				if (this.pingTimeoutCount >= num4)
				{
					Protector.ExitGame();
				}
			}
			if (flag)
			{
				this.pingTimeoutCount = 0;
			}
			if (Settings.Default.healingPotion && !flag && (double)(100 * num2 / ((num3 <= 0) ? 1 : num3)) < Settings.Default.healingPotionValue && (DateTime.Now - this.lastHealingTime).TotalSeconds > 5.0)
			{
				this.lastHealingTime = DateTime.Now;
				Process[] processes = Process.GetProcesses();
				for (int i = 0; i < processes.Length; i++)
				{
					Process process = processes[i];
					if (process.MainWindowTitle == "Diablo II" && process.MainWindowHandle != IntPtr.Zero)
					{
						MainWindow.mainInstance.RemoteThread(MainWindow.mainInstance.g_hD2Client + 276592);
						WinApi.SetForegroundWindow(process.MainWindowHandle);
						SendKeys.SendWait(this.random.Next(2, 5).ToString());
					}
				}
			}
			if ((DateTime.Now - this.lastPrintTime).TotalSeconds > 3.0)
			{
				bool flag2 = false;
				this.lastPrintTime = DateTime.Now;
				StringBuilder stringBuilder = new StringBuilder();
				if (Settings.Default.lifeProtect)
				{
					stringBuilder.Append("生命保护");
				}
				if (Settings.Default.healingPotion)
				{
					stringBuilder.Append(" 药水保护");
				}
				if (Settings.Default.pingProtect)
				{
					stringBuilder.Append(" Ping保护:");
					stringBuilder.Append(this.lastPing);
					if (this.pingTimeoutCount > 0)
					{
						stringBuilder.Append(" TimeOut:");
						stringBuilder.Append(this.pingTimeoutCount);
						flag2 = true;
					}
				}

			}
		}
	}
}
