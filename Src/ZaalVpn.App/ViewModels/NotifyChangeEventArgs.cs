namespace ZaalVpn.App.ViewModels
{
    public class NotifyChangeEventArgs(string message,bool isConnect) : EventArgs
    {
        public readonly string Message = message;
        public readonly bool IsConnect = isConnect;
    }
    public class NotifyErrorChangeEventArgs(string message) : EventArgs
    {
        public readonly string Message = message;
    }

}
