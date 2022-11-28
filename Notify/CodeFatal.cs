using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Notify
{
    internal struct CodeFatal
    {
        public CodeFatal() { }
        internal static readonly NotifyMessage AppDirError = new(1_000, "Application Directory Error", "Application Directory Could Not Be Found Or Created.");
        internal static readonly NotifyMessage AppNotInitError = new(1_001, "Not Initialized Error", "Application Was Not Initialized Before Method Calls. Make Sure To Initialize This Tool Before Using It's Methods.");
        internal static readonly NotifyMessage ToolDirError = new(1_002, "Tool Directory Error", "Calling Service / Tool Directory Could Not Be Found Or Created.");
    }
}
