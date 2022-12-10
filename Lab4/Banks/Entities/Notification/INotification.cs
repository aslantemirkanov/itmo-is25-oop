namespace Banks.Entities.Notification;

public interface INotification
{
    void Send(Client client, string message);
}