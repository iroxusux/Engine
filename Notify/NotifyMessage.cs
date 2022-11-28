using Engine.Core;

namespace Engine.Notify
{
    public class NotifyMessage : Enumeration<NotifyMessage>
    {
        private readonly string _Message;
        public string Message { get { return _Message; } }
        public NotifyMessage(int iValue, string sTitle, string sMessage) : base(iValue, sTitle) { this._Message = sMessage; }
    }
}
