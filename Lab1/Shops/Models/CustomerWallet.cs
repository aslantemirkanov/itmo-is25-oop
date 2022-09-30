namespace Shops.Models;

public class CustomerWallet
{
    public CustomerWallet(int amountMoney)
    {
        AmountMoney = amountMoney;
    }

    public int AmountMoney { get; }
}