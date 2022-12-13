namespace Engine
{
    /// <summary>
    /// Managing Static Class For Studio Development
    /// All Sub Functions Of Studio Should Rely On This Static Main Class To Manage Resources Appropriately
    /// </summary>
    public static class StudioEngine
    {
        /// <summary>
        /// Main String Identifying Studio Environment
        /// </summary>
        private const string APP_NAME = "irox_Studio";
        /// <summary>
        /// Application Directory Path
        /// </summary>
        private static string _AppDirPath { get; set; } = String.Empty;
        /// <summary>
        /// Internal Memory That We Have Initialized
        /// </summary>
        private static bool _Initialized { get; set; } = false;
        /// <summary>
        /// Initialize This Static Managing Class
        /// </summary>
        public static void Init()
        {
            if (_Initialized)
            {
                return;
            }
            CheckStudioDir();
            _Initialized = true;
        }
        public static string Init(string sToolName)
        {
            if (!_Initialized)
            {
                CheckStudioDir();
                _Initialized = true;
            }
            return CheckToolDir(sToolName);
        }
        /// <summary>
        /// Validate That A Calling Tool Has Allocated Space Resolved In Application Directory
        /// </summary>
        /// <param name="sToolName"></param>
        /// <returns></returns>
        public static string CheckToolDir(string sToolName)
        {
            if (!_Initialized) { Notify.NotifyHandler.Fatal(Notify.CodeFatal.AppNotInitError); }
            string sToolPath = Path.Combine(_AppDirPath, sToolName);
            if (!Directory.Exists(sToolPath))
            {
                try
                {
                    Directory.CreateDirectory(sToolPath);
                }
                catch (Exception)
                {
                    Notify.NotifyHandler.Fatal(Notify.CodeFatal.ToolDirError, false);
                }
            }
            return sToolPath;
        }
        /// <summary>
        /// Validate Studio Directory
        /// </summary>
        private static void CheckStudioDir()
        {
            _AppDirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), APP_NAME);
            if (!Directory.Exists(_AppDirPath))
            {
                try
                {
                    Directory.CreateDirectory(_AppDirPath);
                }
                catch (Exception)
                {
                    Notify.NotifyHandler.Fatal(Notify.CodeFatal.AppDirError);
                }
            }
        }
        public static void BindToolToForm<T>(ref Tool.Tool<T> tool, ref T form) where T : class
        {
            tool.BindToForm(ref form);
            tool.Init();
        }
        public static void BindToolToForm<T, F>(ref T tool, ref F form) where T : Tool.Tool<F> where F : class
        {
            tool.BindToForm(ref form);
            tool.Init();
        }
    }
}