using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Shop
{
    private readonly List<ShopProduct> _shopProducts;

    public Shop(ShopId shopId, ShopName shopName, ShopAddress shopAddress)
    {
        ShopId = shopId;
        ShopName = shopName;
        ShopAddress = shopAddress;
        _shopProducts = new List<ShopProduct>();
    }

    public ShopId ShopId { get; }
    public ShopName ShopName { get; }
    public ShopAddress ShopAddress { get; }

    public ShopProduct RegisterProduct(string productName, decimal productPrice = 0, int productCount = 0)
    {
        var newProduct = new ShopProduct(new Product(new ProductName(productName)), productPrice, productCount);
        ShopProduct? findProduct = FindProduct(newProduct);
        if (findProduct != null)
        {
            if (productCount != 0)
            {
                findProduct.ProductCount = productCount;
            }

            findProduct.ProductPrice = productPrice;
            return findProduct;
        }
        else
        {
            _shopProducts.Add(newProduct);
            return newProduct;
        }
    }

    public void AddProduct(ShopProduct shopProduct)
    {
        ShopProduct? findProduct = FindProduct(shopProduct);
        if (findProduct == null)
        {
            RegisterProduct(shopProduct.Product.ProductName.Name, shopProduct.ProductPrice, shopProduct.ProductCount);
        }
        else
        {
            if (shopProduct.ProductCount != 0)
            {
                findProduct.ProductCount = shopProduct.ProductCount;
            }

            findProduct.ProductPrice = shopProduct.ProductPrice;
        }
    }

    public ShopProduct? FindProduct(ShopProduct productToFind)
    {
        return _shopProducts.FirstOrDefault(product =>
            product.Product.ProductName.Equals(productToFind.Product.ProductName));
    }

    public void BuyProduct(ShopProduct shopProduct, int productCount, decimal balance)
    {
        ShopProduct? findProduct = FindProduct(shopProduct);
        if (findProduct == null)
        {
            throw new PurchaseNonExistProductException(shopProduct.Product.ProductName);
        }

        if (shopProduct.ProductPrice * productCount > balance)
        {
            throw new LackOfMoneyException(balance);
        }

        if (shopProduct.ProductCount < productCount)
        {
            throw new NotEnoughProductException(productCount, shopProduct.Product.ProductName);
        }

        shopProduct.ProductCount -= productCount;
    }

    public void BuyBatch(Batch batch, decimal balance)
    {
        if (balance < batch.BatchValue)
        {
            throw new LackOfMoneyException(balance);
        }

        for (int i = 0; i < batch.ShopProducts.Count; ++i)
        {
            ShopProduct? findProduct = FindProduct(batch.ShopProducts[i]);
            if (findProduct == null)
            {
                throw new PurchaseNonExistProductException();
            }

            if (findProduct.ProductCount < batch.ProductBuyAmount[i])
            {
                Console.WriteLine(findProduct.ProductCount);
                Console.WriteLine("\n");
                Console.WriteLine(batch.ProductBuyAmount[i]);
                throw new NotEnoughProductException();
            }
        }

        for (int i = 0; i < batch.ShopProducts.Count; ++i)
        {
            ShopProduct? findProduct = FindProduct(batch.ShopProducts[i]);
            if (findProduct != null)
            {
                findProduct.ProductCount -= batch.ProductBuyAmount[i];
            }
        }
    }

    public void BatchDelivery(List<ShopProduct> batch)
    {
        for (int i = 0; i < batch.Count; ++i)
        {
            ShopProduct? findProduct = FindProduct(batch[i]);
            if (findProduct == null)
            {
                RegisterProduct(batch[i].Product.ProductName.Name);
            }
            else
            {
                findProduct.ProductPrice = batch[i].ProductPrice;
                findProduct.ProductCount = batch[i].ProductCount;
            }
        }
    }

    public bool IsProductRegistered(ShopProduct shopProduct)
    {
        return _shopProducts.Any(product => product.Product.ProductName == shopProduct.Product.ProductName);
    }

    public bool IsEnoughProduct(ShopProduct shopProduct, int requiredCount)
    {
        return shopProduct.ProductCount >= requiredCount;
    }
}