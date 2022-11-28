using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core
{
    public static class EnvironmentExit
    {
        public static void Exit(int iExitCode)
        {
            Environment.Exit(iExitCode);
        }
    }
    public struct ExitCode
    {
        public const int SAFE_EXIT = 0;  // Expected Exit Code
        public const int FATAL_EXIT = 1; // Fatal Exit Code - Something Crashed Unexpectedly
        public const int USER_EXIT = 2;  // User Prompted Exit Code
        public const int CORRUPT_DATA_EXIT = 3;  // Corrupt Data Exit Code
    }
}
