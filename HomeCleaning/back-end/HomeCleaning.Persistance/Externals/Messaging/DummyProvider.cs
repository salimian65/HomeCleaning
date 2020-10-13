using System.IO;

namespace HomeCleaning.Persistance.Externals.Messaging
{
    public class DummySmsProvider : ISmsMessageProvider
    {
        public void Send(string address, string message)
        {
            Directory.CreateDirectory("dummymessages/sms/");
            TextWriter writer = new StreamWriter($"dummymessages/sms/{address}.txt");
            writer.WriteAsync(message);
            writer.Close();
        }
    }
    
    public class DummyEmailProvider : IEmailMessageProvider
    {
         public void Send(string address, string message)
        {
            Directory.CreateDirectory("dummymessages/email/");
            TextWriter writer = new StreamWriter($"dummymessages/email/{address}.txt");
            writer.WriteAsync(message);
            writer.Close();
        }
    }

}