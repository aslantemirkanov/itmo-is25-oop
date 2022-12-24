namespace DataAccess.Models.Sources;

public class PhoneSource
{
    public PhoneSource(string phone)
    {
        Phone = phone;
        Id = Guid.NewGuid();
    }

    public string Phone { get; set; }
    public Guid Id { get; set; }

    public SourceType GetSourceType()
    {
        return SourceType.Phone;
    }
}