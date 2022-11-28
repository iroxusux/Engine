using System.Diagnostics;


namespace Engine.Core
{
    public static class NetshCmds
    {
        public static void OpenNetworkConnectionsPanel()
        {
            ProcessStartInfo startInfo = new("NCPA.cpl") { UseShellExecute = true };
            Process.Start(startInfo);
        }
    }
}
