namespace Shops.Exceptions;

public class LackOfMoneyException : CustomerException
{
    public LackOfMoneyException(decimal amountMoney)
        : base($" {amountMoney} doesn't enough to buy products")
    {
    }
}