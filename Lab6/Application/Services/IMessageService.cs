namespace Application.Services;

public interface IMessageService
{
    void SendMessage(Guid idSender, Guid idReceiver, string message);
}