using System.Text.RegularExpressions;
using Shops.Entities;

namespace Shops.Test;

using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;
using Shops.Services;
using Xunit;

public class ShopTests
{
    private ShopManager _shopManager = new ShopManager();

    [Fact]
    public void ShopDeliveryTest()
    {
        Shop shop1 = _shopManager.CreateShop(new ShopId(), new ShopName("shop1"), new ShopAddress("pushkina"));
        int applesBefore = 10;
        var apple = new ShopProduct(new Product(new ProductName("apple")), 5, applesBefore);

        _shopManager.RegisterProduct(apple.Product.ProductName.Name, shop1, 5, applesBefore);

        decimal moneyBefore = 1000;
        var customer = new Customer("aslan", moneyBefore);
        int appleCount = 2;
        customer.BuyProduct(shop1, apple, appleCount);
        Assert.Equal(moneyBefore - customer.GetBalance(), appleCount * apple.ProductPrice);
        Assert.Equal(applesBefore - apple.ProductCount, appleCount);
    }

    [Fact]
    public void SettingAndChangingProductPriseTest()
    {
        int priceBefore = 5;
        int priceAfter = 10;
        var apple = new ShopProduct(new Product(new ProductName("apple")), priceBefore, 2);
        apple.ProductPrice = priceAfter;
        Assert.Equal(apple.ProductPrice, priceAfter);
    }

    [Fact]
    public void FindingShopWithLowestPriceTest()
    {
        Shop shop1 = _shopManager.CreateShop(new ShopId(), new ShopName("shop1"), new ShopAddress("pushkina"));
        Shop shop2 = _shopManager.CreateShop(new ShopId(), new ShopName("shop2"), new ShopAddress("hello"));
        Shop shop3 = _shopManager.CreateShop(new ShopId(), new ShopName("shop3"), new ShopAddress("street"));

        var apple = new ShopProduct(new Product(new ProductName("apple")), 5, 10);
        var pear = new ShopProduct(new Product(new ProductName("pear")), 3, 20);
        var orange = new ShopProduct(new Product(new ProductName("orange")), 10, 5);

        _shopManager.RegisterProduct(apple.Product.ProductName.Name, shop1, 5, 3);
        _shopManager.RegisterProduct(pear.Product.ProductName.Name, shop1, 3, 20);
        _shopManager.RegisterProduct(orange.Product.ProductName.Name, shop1, 10, 5);

        _shopManager.RegisterProduct(apple.Product.ProductName.Name, shop2, 5, 10);
        _shopManager.RegisterProduct(orange.Product.ProductName.Name, shop2, 10, 5);

        _shopManager.RegisterProduct(apple.Product.ProductName.Name, shop3, 5, 10);
        _shopManager.RegisterProduct(pear.Product.ProductName.Name, shop3, 3, 20);

        var newBatch = new Batch();
        newBatch.Add(apple, 3);
        newBatch.Add(pear, 1);
        newBatch.Add(orange, 2);

        Assert.Equal(_shopManager.ShopWithLowestPrice(newBatch), shop1);
    }

    [Fact]
    public void BuyBatchTest()
    {
        Shop shop1 = _shopManager.CreateShop(new ShopId(), new ShopName("shop1"), new ShopAddress("pushkina"));

        var apple = new ShopProduct(new Product(new ProductName("apple")), 5, 10);
        shop1.AddProduct(apple);

        var pear = new ShopProduct(new Product(new ProductName("pear")), 3, 20);
        shop1.AddProduct(pear);

        var orange = new ShopProduct(new Product(new ProductName("orange")), 10, 5);
        shop1.AddProduct(orange);

        var newBatch = new Batch();
        newBatch.Add(apple, 3);
        newBatch.Add(pear, 1);
        newBatch.Add(orange, 2);

        int balanceBefore = 1000;
        var customer = new Customer("aslan", balanceBefore);
        customer.BuyBatch(shop1, newBatch);

        Assert.Equal(balanceBefore - customer.GetBalance(), newBatch.BatchValue);
    }
}