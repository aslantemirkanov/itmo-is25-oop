namespace DataAccess.Models;

public class Message
{
    public Message(Guid sender, Guid receiver, string messageContent, MessageStatus status, DateTime creationTime, Guid id)
    {
        Sender = sender;
        Receiver = receiver;
        MessageContent = messageContent;
        Status = status;
        CreationTime = creationTime;
        Id = id;
    }

#pragma warning disable CS8618
    protected Message() { }
#pragma warning restore CS8618

    public Guid Sender { get; }
    public Guid Receiver { get; }

    public Guid Id { get; set; }

    public DateTime CreationTime { get; }
    public string MessageContent { get; }
    public MessageStatus Status { get; set; }
}