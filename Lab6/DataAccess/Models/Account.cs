using DataAccess.Models.Sources;

namespace DataAccess.Models;

public class Account
{
    public Account(string login, string password, Guid id)
    {
        Workers = new List<Worker>();
        Messages = new List<Message>();
        MailSources = new List<MailSource>();
        MessengerSources = new List<MessengerSource>();
        PhoneSources = new List<PhoneSource>();
        Login = login;
        Password = password;
        Id = id;
    }

#pragma warning disable CS8618
    protected Account() { }
#pragma warning restore CS8618

    public virtual List<Worker> Workers { get; set; }
    public virtual List<Message> Messages { get; set; }

    public virtual List<MailSource> MailSources { get; set; }
    public virtual List<PhoneSource> PhoneSources { get; set; }
    public virtual List<MessengerSource> MessengerSources { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public Guid Id { get; set; }
}