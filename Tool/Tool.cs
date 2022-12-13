using Engine.Notify;

namespace Engine.Tool
{
    /// <summary>
    /// Abstract Class For Subsidiary Tools To Indicon Studio
    /// </summary>
    public abstract class Tool<T> where T: class
    {
        /// <summary>
        /// Name Of Tool - Will Be Used For Directories, Windows, File Naming Schemes, etc.
        /// </summary>
        protected string ToolName = string.Empty;
        /// <summary>
        /// Directory Path Of Tool
        /// </summary>
        public string DirectoryPath { get; protected set; } = string.Empty;
        /// <summary>
        /// Internally Managed Boolean Of Tool Initialized
        /// </summary>
        public bool Initialized { get; private set; } = false;
        /// <summary>
        /// Form Generic For Binding Tool To Passed Windows Form
        /// </summary>
        protected T? Form { get; set; } = null;
        /// <summary>
        /// Default Implimentation Of Init Function - Override As Required
        /// </summary>
        public virtual void Init()
        {
            DirectoryPath = StudioEngine.Init(ToolName);
            Load();
            Initialized = true;
        }
        /// <summary>
        /// Abstract Save Method
        /// Override To Save This Tool's Data To Related Directory
        /// </summary>
        protected abstract void Save();
        /// <summary>
        /// Abstract Load Method
        /// Override To Load This Tool's Data From Related Directory
        /// </summary>
        protected abstract void Load();
        /// <summary>
        /// Native Startup Method Call
        /// Override If Required - This Will Set This Tool To Start Up With Windows
        /// </summary>
        /// <param name="bEnable"></param>
        protected virtual void SetWindowsStartUp(bool bEnable) { NativeWinFuncs.NativeWinFuncs.SetStartup(ToolName, bEnable); }
        /// <summary>
        /// Native Startup Method Call
        /// Override If Rquired - This Will Get The Status Of The Start Up
        /// </summary>
        /// <returns></returns>
        protected virtual bool GetWindowsStartUp() { return NativeWinFuncs.NativeWinFuncs.StartupStatus(ToolName); }
        public virtual void BindToForm(ref T form)
        {
            if (form == null) NotifyHandler.Fatal(CodeFatal.NullFormError);
            Form = form;
        }
    }
}
