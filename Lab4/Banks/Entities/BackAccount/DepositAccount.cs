using System.Data;
using Banks.Entities.Notification;
using Banks.Exceptions;

namespace Banks.Entities.BackAccount;

public class DepositAccount : IBankAccount
{
    private DateTime _closingDate;
    private double _interestRate;
    private Guid _accountId;
    private double _balance;
    private Client _client;
    private AccountType _accountType;
    private Bank _bank;
    private double _interestBank;

    public DepositAccount(Bank bank, Client client, double interestRate, double balance, AccountType accountType)
    {
        _bank = bank;
        _client = client;
        _interestRate = interestRate;
        _balance = balance;
        _accountType = accountType;
        _closingDate = DateTime.Now.AddMonths(3);
        _accountId = Guid.NewGuid();
        _interestBank = 0;
    }

    public void FillUpMoney(double moneyAmount)
    {
        _balance += moneyAmount;
    }

    public void TakeOffMoney(double moneyAmount)
    {
        if (DateTime.Now < _closingDate)
        {
            throw BankException.WithdrawingFromDepositAccount();
        }

        if (_balance < moneyAmount)
        {
            throw BankException.NegativeBalance();
        }

        _balance -= moneyAmount;
    }

    public Guid GetAccountId()
    {
        return _accountId;
    }

    public void AddDailyInterest()
    {
        _interestBank += _balance * (_interestRate / 365);
    }

    public void AddMonthInterest()
    {
        _balance += _interestBank;
        _interestBank = 0;
    }

    public void ChangeDepositInterest(double newDepositInterest)
    {
        _interestRate = newDepositInterest;
    }

    public void ChangeParameter(ChangeType changeType, double newParameter)
    {
        switch (changeType)
        {
            case ChangeType.DepositInterest:
                ChangeDepositInterest(newParameter);
                break;
            case ChangeType.AddMonthInterest:
                AddMonthInterest();
                break;
            case ChangeType.AddDayInterest:
                AddDailyInterest();
                break;
            default:
                throw BankException.NonExistChangeType();
        }
    }

    public double GetBalance()
    {
        return _balance;
    }

    public AccountType GetAccountType()
    {
        return _accountType;
    }
}