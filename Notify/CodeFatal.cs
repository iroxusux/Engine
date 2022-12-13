namespace Engine.Notify
{
    internal struct CodeFatal
    {
        public CodeFatal() { }
        internal static readonly NotifyMessage AppDirError = new(1_000, "Application Directory Error", "Application Directory Could Not Be Found Or Created.");
        internal static readonly NotifyMessage AppNotInitError = new(1_001, "Not Initialized Error", "Application Was Not Initialized Before Method Calls. Make Sure To Initialize This Tool Before Using It's Methods.");
        internal static readonly NotifyMessage ToolDirError = new(1_002, "Tool Directory Error", "Calling Service / Tool Directory Could Not Be Found Or Created.");
        internal static readonly NotifyMessage NullFormError = new(10_003, "Null Form Error", "Null Form Was Passed To Logical Tool, Cannot Continue!");
    }
}
