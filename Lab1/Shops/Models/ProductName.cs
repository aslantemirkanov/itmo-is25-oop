namespace Shops.Models;

public record ProductName
{
    public string Name { get; }
    public ProductName(string productName) => Name = productName;
}