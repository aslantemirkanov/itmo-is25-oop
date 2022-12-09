namespace Banks.Entities.Notification;

public class PhoneNotification : INotification
{
    public void Send(Client client, string message)
    {
        client.GetNotification($"notification by phone: {message}");
        /*https://github.com/flash2048/Patterns_2018/blob/master/Decorator/Decorator/WorkWithText/EditOfTextBase.cs*/
    }
}