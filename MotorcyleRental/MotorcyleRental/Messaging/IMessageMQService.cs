namespace MotorcyleRental.Messaging
{
    public interface IMessageMQService
    {
        void SendMessage(string message);

        IEnumerable<string> ReceiveMessages();
    }
}
