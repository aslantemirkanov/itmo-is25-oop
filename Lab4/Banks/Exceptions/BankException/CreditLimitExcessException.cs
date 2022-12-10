namespace Banks.Exceptions;

public class CreditLimitExcessException : BankException
{
    public CreditLimitExcessException()
        : base("You can't excess credit limit")
    {
    }
}