using DataAccess;
using DataAccess.Models;

namespace Application.Services.Implementations;

public class MessageService : IMessageService
{
    private readonly DatabaseContext _context;

    public MessageService(DatabaseContext context)
    {
        _context = context;
    }

    public void SendMessage(Guid idSender, Guid idReceiver, string message)
    {
        var sender = _context.GetAccount(idSender);
        var receiver = _context.GetAccount(idReceiver);
        var newMessage = new Message(idSender, idReceiver, message, MessageStatus.Unread, DateTime.Now, Guid.NewGuid());
        sender?.Messages.Add(newMessage);
        receiver?.Messages.Add(newMessage);
        _context.Messages.Add(newMessage);
        _context.SaveChanges();
    }
}