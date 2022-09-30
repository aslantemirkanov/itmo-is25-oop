using Shops.Models;

namespace Shops.Entities;

public class Shop
{
    private readonly List<Product> _shopCatalog;

    public Shop(ShopId shopId, ShopName shopName, ShopAddress shopAddress)
    {
        ShopId = shopId;
        ShopName = shopName;
        ShopAddress = shopAddress;
        _shopCatalog = new List<Product>();
    }

    public ShopId ShopId { get; }
    public ShopName ShopName { get; }
    public ShopAddress ShopAddress { get; }
}