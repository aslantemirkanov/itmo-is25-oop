namespace Shops.Models;

public class ShopId
{
    public ShopId()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
}