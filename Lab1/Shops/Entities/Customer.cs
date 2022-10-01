using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Customer
{
    private string _name;
    private decimal _balance;

    public Customer(string name, decimal balance)
    {
        _name = name;
        _balance = balance;
    }

    public void BuyProduct(Shop shop, ShopProduct shopProduct, int productCount)
    {
        shop.BuyProduct(shopProduct, productCount, _balance);
        _balance -= productCount * shopProduct.ProductPrice;
    }

    public void BuyBatch(Shop shop, Batch batch)
    {
        shop.BuyBatch(batch, _balance);
        _balance -= batch.BatchValue;
    }

    public decimal GetBalance()
    {
        return _balance;
    }
}