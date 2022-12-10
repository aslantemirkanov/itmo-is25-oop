namespace Banks.Exceptions;

public class NegativeBalanceException : BankException
{
    public NegativeBalanceException()
        : base("Not enough money in the account")
    {
    }
}