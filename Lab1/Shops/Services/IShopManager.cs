using Shops.Entities;
/*using Shops.Exceptions;*/
using Shops.Models;

namespace Shops.Services;

public interface IShopManager
{
    public void RegisterProduct(string productName, Shop shop,  decimal productPrice = 0, int productCount = 0);
    public Shop CreateShop(ShopId shopId, ShopName shopName, ShopAddress shopAddress);
    Shop ShopWithLowestPrice(Batch batch);
}