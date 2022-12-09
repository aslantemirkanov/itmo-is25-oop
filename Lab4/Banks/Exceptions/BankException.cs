using System.Threading.Tasks.Dataflow;
using Banks.Entities.BackAccount;

namespace Banks.Exceptions;

public class BankException : Exception
{
    private BankException(string message)
        : base(message)
    {
    }

    public static BankException WrongDepositBalance(double balance)
    {
        return new BankException(
            $"You can't create deposit account with balance {balance}, because bank haven't that rate");
    }

    public static BankException WrongAccountId(Guid accountId)
    {
        return new BankException($"Account with id {accountId} doesn't exist");
    }

    public static BankException NegativeBalance()
    {
        return new BankException("Not enough money in the account");
    }

    public static BankException WithdrawingFromDepositAccount()
    {
        return new BankException("You cant withdraw money from deposit account while it's exist");
    }

    public static BankException NonExistTransaction()
    {
        return new BankException("That transaction doesn't exist");
    }

    public static BankException NonExistAccountType()
    {
        return new BankException("That account type doesn't exist");
    }

    public static BankException CreditLimitExcess()
    {
        return new BankException("You can't excess credit limit");
    }

    public static BankException NonExistChangeType()
    {
        return new BankException("You want to change something strange");
    }

    public static BankException TryToCreateVerificatedAccount()
    {
        return new BankException("You can't create credit account for unveficated client ");
    }

    public static BankException ExtraWithdrawalLimit(double limit)
    {
        return new BankException("You can't take off more than limit");
    }
}