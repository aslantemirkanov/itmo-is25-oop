using Shops.Entities;
using Shops.Exceptions;

namespace Shops.Models;

public class Batch
{
    public Batch()
    {
        ShopProducts = new List<ShopProduct>();
        ProductBuyAmount = new List<int>();
    }

    public decimal BatchValue { get; private set; }
    public List<ShopProduct> ShopProducts { get; }
    public List<int> ProductBuyAmount { get; set; }

    public void Add(ShopProduct shopProduct, int productBuyAmount)
    {
        if (productBuyAmount < 1)
        {
            throw new NotEnoughProductException(productBuyAmount, shopProduct.Product.ProductName);
        }

        ShopProducts.Add(shopProduct);
        ProductBuyAmount.Add(productBuyAmount);
        BatchValue += productBuyAmount * shopProduct.ProductPrice;
    }
}