using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static DMTools.NomadMemory;
using static DMTools.WinApi;
using static DMTools.Util;



namespace DMTools
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class MainWindow
    {
        public IntPtr g_pD2sgpt, g_pD2InjectPrint, g_pD2InjectString, g_pD2InjectGetString;
        public static MainWindow mainInstance;
        uint g_iD2pid;
        public MainWindow()
        {
            mainInstance = this;
            Init();

        }
        Protector protector = new Protector();
        public void Main()
        {
            //AutoItApi._HotKey_Disable(HK_FLAG_D2STATS);

            //var hTimerUpdateDelay = AutoItApi.TimerInit();

            int timer = 0;

            bool bIsIngame = false;

            while (true)
            {
                timer++;

                var hWnd = AutoItApi.WinGetHandle("Diablo II");
                if (hWnd == (IntPtr)0)
                {
                    Thread.Sleep(500);
                    //ErrorMsg = "Couldn't find Diablo II window";
                    continue;
                }

                try
                {
                    UpdateHandle(hWnd);
                    //ErrorMsg = "";
                }
                catch (Exception ex)
                {
                    //ErrorMsg = ex.Message;
                }


                if (IsIngame())
                {

                    InjectFunctions();


                    protector.Do();




                    bIsIngame = true;
                }
                else
                {
                    if (bIsIngame)
                    {
                        //AutoItApi.GUICtrlSetState(g_idnotifyTest, GUI_DISABLE);
                    }

                    bIsIngame = false;
                }


                Thread.Sleep(200);
            }
        }
        public  Tuple<int, int> GetLifeStatu()
        {
            return protector.GetLifeStatu();
        }
        public void Init()
        {
            var hWnd = AutoItApi.WinGetHandle("Diablo II");
            if (hWnd == (IntPtr)0)
            {
                Thread.Sleep(500);
                //ErrorMsg = "Couldn't find Diablo II window";
                return;
            }

            try
            {
                UpdateHandle(hWnd);
                //ErrorMsg = "";
            }
            catch (Exception ex)
            {
                //ErrorMsg = ex.Message;
            }


            if (IsIngame())
            {

                InjectFunctions();






            }
        }
        public void Do()
        {
            var hWnd = AutoItApi.WinGetHandle("Diablo II");
            if (hWnd == (IntPtr)0)
            {
                Thread.Sleep(500);
                //ErrorMsg = "Couldn't find Diablo II window";
                return;
            }

            try
            {
                UpdateHandle(hWnd);
                //ErrorMsg = "";
            }
            catch (Exception ex)
            {
                //ErrorMsg = ex.Message;
            }


            if (IsIngame())
            {

                InjectFunctions();


                protector.Do();




            }

        }
        public bool IsIngame()
        {
            if (g_iD2pid == 0)
            {
                return false;
            }
            return MemoryRead(g_hD2Client + 0x11BBFC, g_ahD2Handle) != 0;
        }
        public static int MemoryRead(IntPtr iv_Address, IntPtr ah_Handle, string sv_Type = "dword")
        {
            var v_Buffer = AutoItApi.DllStructCreate(sv_Type);

            int numberBytesRead = 0;
            ReadProcessMemory(ah_Handle, iv_Address, v_Buffer, AutoItApi.DllStructGetSize(v_Buffer), out numberBytesRead);

            var v_Value = AutoItApi.DllStructGetData(v_Buffer, 1, sv_Type);
            return v_Value;
        }
        public uint RemoteThread(IntPtr pFunc)
        {
            // $var is in EBX register
            return RemoteThread(pFunc, IntPtr.Zero);
        }
        public void _CloseHandle()
        {
            if (g_ahD2Handle != IntPtr.Zero)
            {
                MemoryClose(g_ahD2Handle);
                g_ahD2Handle = IntPtr.Zero;
                g_iD2pid = 0;
            }
        }
        uint g_iUpdateFailCounter;
        public void UpdateHandle(IntPtr hWnd)
        {

            var iPID = AutoItApi.WinGetProcess(hWnd);

            //if (iPID == -1) { return _CloseHandle(); }
            if (iPID == g_iD2pid)
            {
                // Already initialized
                return;
            }

            _CloseHandle();
            g_iUpdateFailCounter += 1;
            g_ahD2Handle = OpenProcess((int)iPID);
            if (g_ahD2Handle == IntPtr.Zero)
            {
                // https://docs.microsoft.com/en-au/windows/win32/debug/system-error-codes
                var lastWin32Error = Marshal.GetLastWin32Error();
                throw new Exception($"UpdateHandle: Couldn't open Diablo II memory handle. No Admin rights? lastWin32Error: {lastWin32Error}");
            }

            if (!UpdateDllHandles())
            {
                _CloseHandle();
                //logger.Debug("UpdateHandle: Couldn't update dll handles.");
                //throw new Exception("UpdateHandle: Couldn't update dll handles.");
            }

            if (InjectFunctions() == false)
            {
                _CloseHandle();
                throw new Exception("UpdateHandle: Couldn't inject functions.");
            }

            g_iUpdateFailCounter = 0;
            g_iD2pid = iPID;
            g_pD2sgpt = (IntPtr)MemoryRead(g_hD2Common + 0x99E1C, g_ahD2Handle);
        }
        public static string SwapEndian(IntPtr pAddress)
        {
            var bytes = BitConverter.GetBytes(pAddress.ToInt32());
            Array.Reverse(bytes);
            int result = BitConverter.ToInt32(bytes, 0);

            return result.ToString("X4");
        }
        public bool InjectCode(IntPtr pWhere, string sCode)
        {
            MemoryWriteHexString(pWhere, g_ahD2Handle, sCode);

            var iConfirm = MemoryRead(pWhere, g_ahD2Handle);
            //throw new Exception("Den vergleich unterhalb nochmal genau anschauen was da abgeht");
            return SwapEndian((IntPtr)iConfirm) == sCode.Substring(2, 8);
        }
        public bool InjectFunctions()
        {
            var iPrintOffset = IntPtr.Subtract((g_hD2Client + 0x7D850), (g_hD2Client + 0xCDE0D).ToInt32());

            var sWrite = "0x5368" + SwapEndian(g_pD2InjectString) + "31C0E8" + SwapEndian(iPrintOffset) + "C3";
            var bPrint = InjectCode(g_pD2InjectPrint, sWrite);

            sWrite = "0x8BCB31C0BB" + SwapEndian(g_hD2Lang + 0x9450) + "FFD3C3";
            var bGetString = InjectCode(g_pD2InjectGetString, sWrite);

            return bPrint && bGetString;
        }
        public bool UpdateDllHandles()
        {
            var pLoadLibraryW = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryW");
            if (IntPtr.Zero == pLoadLibraryW) { throw new Exception("UpdateDllHandles: Couldn't retrieve LoadLibraryA address."); }

            //var pAllocAddress = _MemVirtualAllocEx(g_ahD2Handle[1], 0, 0x100, BitOR(MEM_COMMIT, MEM_RESERVE), PAGE_EXECUTE_READWRITE);
            var pAllocAddress = VirtualAllocEx(g_ahD2Handle, IntPtr.Zero, 0x100, AllocationType.Commit | AllocationType.Reserve, MemoryProtection.ExecuteReadWrite);
            if (pAllocAddress == IntPtr.Zero)
            {
                // https://docs.microsoft.com/en-au/windows/win32/debug/system-error-codes
                var lastWin32Error = Marshal.GetLastWin32Error();
                throw new Exception("UpdateDllHandles: Failed to allocate memory.");
            }

            string[] g_asDLL = { "D2Client.dll", "D2Common.dll", "D2Win.dll", "D2Lang.dll" };

            var iDLLs = g_asDLL.Length;
            IntPtr[] hDLLHandle = new IntPtr[iDLLs];
            var bFailed = false;

            for (int i = 0; i < iDLLs; i++)
            {
                MemoryWrite(pAllocAddress, g_ahD2Handle, g_asDLL[i] + '\0');
                hDLLHandle[i] = (IntPtr)RemoteThread(pLoadLibraryW, pAllocAddress);
                if (hDLLHandle[i] == IntPtr.Zero)
                {
                    bFailed = true;
                }
            }

            g_hD2Client = hDLLHandle[0];
            g_hD2Common = hDLLHandle[1];
            g_hD2Win = hDLLHandle[2];
            g_hD2Lang = hDLLHandle[3];

            var pD2Inject = g_hD2Client + 0xCDE00;
            g_pD2InjectPrint = pD2Inject + 0x0;
            g_pD2InjectGetString = pD2Inject + 0x10;
            g_pD2InjectString = pD2Inject + 0x20;

            g_pD2sgpt = (IntPtr)MemoryRead(g_hD2Common + 0x99E1C, g_ahD2Handle);

            //_MemVirtualFreeEx(g_ahD2Handle[1], pAllocAddress, 0x100, MEM_RELEASE);
            var tets = Marshal.GetLastWin32Error();
            var freeRet = VirtualFreeEx(g_ahD2Handle, pAllocAddress, 0, FreeType.Release);
            var tets2 = Marshal.GetLastWin32Error();
            if (freeRet == false)
            {
                throw new Exception("UpdateDllHandles: Failed to free memory.");
            }
            if (bFailed)
            {
                //logger.Debug("UpdateDllHandles: Couldn't retrieve dll addresses.");
                //throw new Exception("UpdateDllHandles: Couldn't retrieve dll addresses.");
                return false;
            }

            return true;
        }

        public uint RemoteThread(IntPtr pFunc, IntPtr iVar)
        {
            // $var is in EBX register

            //var aResult = DllCall(g_ahD2Handle[0], "ptr", "CreateRemoteThread", "ptr", g_ahD2Handle[1], "ptr", 0, "uint", 0, "ptr", pFunc, "ptr", iVar, "dword", 0, "ptr", 0);
            var aResult = CreateRemoteThread(g_ahD2Handle, (IntPtr)0, 0, pFunc, (IntPtr)iVar, 0, (IntPtr)0);
            var hThread = aResult;
            if (hThread == IntPtr.Zero) { throw new Exception("RemoteThread: Couldn't create remote thread."); }

            WaitForSingleObject(hThread);

            //var tDummy = AutoItApi.DllStructCreate("dword");
            uint lpExitCode;
            // NOTE: Eventuell muss ich aber die intere variante von GetExitCodeThread in d2 verwenden? https://docs.microsoft.com/en-us/archive/blogs/jonathanswift/dynamically-calling-an-unmanaged-dll-from-net-c
            //DllCall(g_ahD2Handle[0], "bool", "GetExitCodeThread", "handle", hThread, "ptr", AutoItApi.DllStructGetPtr(tDummy));
            var test = GetExitCodeThread(hThread, out lpExitCode);
            //var iRet = Dec(AutoItApi.Hex(AutoItApi.DllStructGetData(tDummy, 1)));
            var iRet = lpExitCode;

            CloseHandle(hThread);
            return iRet;
        }

        public IntPtr g_hD2Client, g_hD2Common, g_hD2Win, g_hD2Lang;
        public IntPtr g_ahD2Handle;

    }
}
