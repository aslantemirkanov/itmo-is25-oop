namespace DataAccess.Models.Sources;

public class MessengerSource
{
    public MessengerSource(string messeger)
    {
        Messeger = messeger;
        Id = Guid.NewGuid();
    }

    public string Messeger { get; set; }
    public Guid Id { get; set; }

    public SourceType GetSourceType()
    {
        return SourceType.Messenger;
    }
}