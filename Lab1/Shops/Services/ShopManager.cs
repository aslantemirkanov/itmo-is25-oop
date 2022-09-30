namespace Shops.Services;

using Shops.Entities;
/*using Shops.Exceptions;*/
using Shops.Models;

public class ShopManager : IShopManager
{
    private readonly List<Shop> _shops;

    public ShopManager()
    {
        _shops = new List<Shop>();
    }

    public Product RegisterProduct()
    {
        throw new NotImplementedException();
    }

    public Shop CreateShop(ShopId shopId, ShopName shopName, ShopAddress shopAddress)
    {
        var shop = new Shop(shopId, shopName, shopAddress);
        _shops.Add(shop);
        return shop;
    }
}