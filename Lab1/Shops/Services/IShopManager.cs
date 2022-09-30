using Shops.Entities;
/*using Shops.Exceptions;*/
using Shops.Models;

namespace Shops.Services;

public interface IShopManager
{
    public Product RegisterProduct();
    public Shop CreateShop(ShopId shopId, ShopName shopName, ShopAddress shopAddress);
}