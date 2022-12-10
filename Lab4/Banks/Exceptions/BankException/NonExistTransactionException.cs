namespace Banks.Exceptions;

public class NonExistTransactionException : BankException
{
    public NonExistTransactionException()
        : base("That transaction doesn't exist")
    {
    }
}