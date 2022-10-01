using Shops.Exceptions;

namespace Shops.Entities;

public class ShopProduct
{
    private decimal _productPrise;

    public ShopProduct(Product product, decimal productPrice, int productCount)
    {
        if (productPrice < 0)
        {
            throw new NegativeProductPriseException(productPrice);
        }

        if (productCount < 0)
        {
            throw new NegativeProductCountException(productCount);
        }

        Product = product;
        _productPrise = productPrice;
        ProductCount = productCount;
    }

    public Product Product { get; }

    public decimal ProductPrice
    {
        get => _productPrise;
        set
        {
            if (value < 0)
            {
                throw new NegativeProductPriseException(value);
            }

            _productPrise = value;
        }
    }

    public int ProductCount { get; set; }
}