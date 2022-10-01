namespace Shops.Models;

public record ProductName
{
    public ProductName(string productName)
    {
        Name = productName;
    }

    public string Name { get; }
}