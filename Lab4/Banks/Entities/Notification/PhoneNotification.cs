namespace Banks.Entities.Notification;

public class PhoneNotification : INotification
{
    public void Send(Client client, string message)
    {
        client.GetNotification($"notification by phone: {message}");
    }
}