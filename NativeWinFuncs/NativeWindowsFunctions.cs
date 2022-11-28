using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Engine.NativeWinFuncs
{
    public static class NativeWinFuncs
    {
        public const int STD_INPUT_HANDLE = -10;
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
            public static implicit operator Point(POINT oPoint)
            {
                return new Point(oPoint.X, oPoint.Y);
            }
        }
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);
        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            return lpPoint;
        }
        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(uint destIP, uint srcIP, byte[] macAddress, ref uint macAddressLength);
        public static byte[] GetMacAddress(IPAddress address)
        {
            byte[] mac = new byte[6];
            uint len = (uint)mac.Length;
            byte[] addressBytes = address.GetAddressBytes();
            uint dest = ((uint)addressBytes[3] << 24)
              + ((uint)addressBytes[2] << 16)
              + ((uint)addressBytes[1] << 8)
              + ((uint)addressBytes[0]);
            if (SendARP(dest, 0, mac, ref len) != 0)
            {
                return Array.Empty<byte>();
            }
            return mac;
        }
        public static void SetStartup(string sAppName, bool bEnabled)
        {
            RegistryKey oReg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (bEnabled)
            {
                oReg.SetValue(sAppName, Application.ExecutablePath.ToString());
                CopyToStartupFolder();
            }
            else
            {
                oReg.DeleteValue(sAppName, false);
            }
        }
        public static bool StartupStatus(string sAppName)
        {
            RegistryKey ? oReg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", false);
            if(oReg == null)
            {
                return false;
            }
            var oVal = oReg.GetValue(sAppName);
            if(oVal == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CancelIoEx(IntPtr handle, IntPtr lpOverlapped);
        public static void CopyToStartupFolder()
        {
            return; // not fully developed yet
            string pathStartUp = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup);
            var exe = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            if (exe == null) return;
            if (exe.EndsWith(".exe"))
            {
                var destiny = Path.Combine(pathStartUp, Path.GetFileName(exe));
                var data = File.ReadAllBytes(exe);
                File.WriteAllBytes(destiny, data);
            }
        }
    }
}
