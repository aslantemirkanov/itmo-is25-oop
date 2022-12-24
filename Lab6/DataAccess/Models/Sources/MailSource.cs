namespace DataAccess.Models.Sources;

public class MailSource
{
    public MailSource(string mail)
    {
        Mail = mail;
        Id = Guid.NewGuid();
    }

    public string Mail { get; set; }
    public Guid Id { get; set; }

    public SourceType GetSourceType()
    {
        return SourceType.Mail;
    }
}