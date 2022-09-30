namespace Shops.Entities;

public class Customer
{
    private string _name;
    private int _amountMoney;

    public Customer(string name, int amountMoney)
    {
        _name = name;
        _amountMoney = amountMoney;
    }

    public void Buy(Shop shop, Product product, int productCount)
    {
    }
}