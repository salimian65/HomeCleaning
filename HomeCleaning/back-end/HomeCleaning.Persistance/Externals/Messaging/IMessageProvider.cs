namespace HomeCleaning.Persistance.Externals.Messaging
{
    public interface IMessageProvider
    {
        void Send(string address, string message);
    }
}