namespace Banks.Exceptions;

public class WrongAccountIdException : BankException
{
    public WrongAccountIdException(Guid accountId)
        : base($"Account with id {accountId} doesn't exist")
    {
    }
}