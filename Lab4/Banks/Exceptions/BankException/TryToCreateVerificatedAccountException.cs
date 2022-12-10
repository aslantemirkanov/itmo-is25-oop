namespace Banks.Exceptions;

public class TryToCreateVerificatedAccountException : BankException
{
    public TryToCreateVerificatedAccountException()
        : base("You can't create credit account for unveficated client ")
    {
    }
}