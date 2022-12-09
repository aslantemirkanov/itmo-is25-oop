namespace Banks.Entities.Notification;

public class MailNotification : INotification
{
    private INotification _previousNotificator;

    public MailNotification(INotification previousNotificator)
    {
        _previousNotificator = previousNotificator;
    }

    public void Send(Client client, string message)
    {
        client.GetNotification($"notification by mail: {message}");
        _previousNotificator.Send(client, message);
    }
}