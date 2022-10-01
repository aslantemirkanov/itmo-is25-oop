namespace Shops.Services;

using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;

public class ShopManager : IShopManager
{
    private readonly List<Shop> _shops;

    public ShopManager()
    {
        _shops = new List<Shop>();
    }

    public void RegisterProduct(string productName, Shop shop,  decimal productPrice = 0, int productCount = 0)
    {
        shop.RegisterProduct(productName, productPrice, productCount);
    }

    public Shop CreateShop(ShopId shopId, ShopName shopName, ShopAddress shopAddress)
    {
        var shop = new Shop(shopId, shopName, shopAddress);
        _shops.Add(shop);
        return shop;
    }

    public Shop ShopWithLowestPrice(Batch batch)
    {
        decimal currentPrice = 0;
        decimal lowestPrice = 0;
        Shop? shopWithLowestPrice = null;
        foreach (var shop in _shops)
        {
            bool isFullBatch = true;
            for (int i = 0;
                 i < batch.ShopProducts.Count;
                 ++i)
            {
                ShopProduct product = batch.ShopProducts[i];
                if (shop.IsProductRegistered(product) && shop.IsEnoughProduct(product, batch.ProductBuyAmount[i]))
                {
                    currentPrice += product.ProductPrice * batch.ProductBuyAmount[i];
                }
                else
                {
                    isFullBatch = false;
                }
            }

            if (isFullBatch && (currentPrice < lowestPrice || lowestPrice == 0))
            {
                lowestPrice = currentPrice;
                shopWithLowestPrice = shop;
            }
        }

        if (shopWithLowestPrice == null)
        {
            throw new NoBatchException();
        }

        return shopWithLowestPrice;
    }
}