using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Engine.Notify
{
    public static class NotifyHandler
    {
        public static void General(NotifyMessage oMessage)
        {
            MessageBox.Show($"Code: {oMessage.Value}: {oMessage.Message}", oMessage.DisplayName);
        }
        public static void Fatal(NotifyMessage oMessage, bool bEnvExit = true)
        {
            General(oMessage);
            if(bEnvExit) { Environment.Exit(oMessage.Value); }
        }
    }
}
