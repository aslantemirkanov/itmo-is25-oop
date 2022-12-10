using Banks.Entities.Notification;
using Banks.Entities.Transaction;
using Banks.Exceptions;

namespace Banks.Entities.BackAccount;

public class CreditAccount : IBankAccount
{
    private double _balance;
    private double _interestRate;
    private double _creditLimit;
    private double _lendMoney;
    private Guid _accountId;
    private DateTime _closingDate;
    private Client _client;
    private AccountType _accountType;
    private Bank _bank;
    private List<TransactionLog> _transactions;

    public CreditAccount(
        Bank bank,
        Client client,
        double interestRate,
        double creditLimit,
        double balance,
        AccountType accountType)
    {
        _bank = bank;
        _client = client;
        _balance = balance;
        _interestRate = interestRate;
        _creditLimit = creditLimit;
        _accountId = Guid.NewGuid();
        _closingDate = DateTime.Now.AddMonths(3);
        _accountType = accountType;
        _lendMoney = 0;
        _transactions = new List<TransactionLog>();
    }

    public void FillUpMoney(double moneyAmount)
    {
        if (_lendMoney == 0)
        {
            _balance += moneyAmount;
        }
        else
        {
            if (moneyAmount >= _lendMoney)
            {
                _balance = moneyAmount - _lendMoney;
                _lendMoney = 0;
            }
            else
            {
                _balance = 0;
                _lendMoney -= moneyAmount;
            }
        }
    }

    public void TakeOffMoney(double moneyAmount)
    {
        if (_balance - moneyAmount >= 0)
        {
            _balance -= moneyAmount;
        }
        else
        {
            if (Math.Abs(_balance - moneyAmount) + _lendMoney + _interestRate > _creditLimit)
            {
                throw new CreditLimitExcessException();
            }

            _lendMoney += Math.Abs(_balance - moneyAmount) + _interestRate;
            _balance = 0;
        }
    }

    public Guid GetAccountId()
    {
        return _accountId;
    }

    public void ChangeCreditLimit(double newCreditLimit)
    {
        _creditLimit = newCreditLimit;
    }

    public void ChangeCreditInterest(double newCreditInterest)
    {
        _interestRate = newCreditInterest;
    }

    public void ChangeParameter(ChangeType changeType, double newParameter)
    {
        switch (changeType)
        {
            case ChangeType.CreditInterest:
                ChangeCreditInterest(newParameter);
                break;
            case ChangeType.CreditLimit:
                ChangeCreditLimit(newParameter);
                break;
            default:
                throw new NonExistChangeTypeException();
        }
    }

    public double GetBalance()
    {
        return _balance;
    }

    public void AddTransactionLog(TransactionLog transactionLog)
    {
        _transactions.Add(transactionLog);
    }

    public List<TransactionLog> GetTransactions()
    {
        return _transactions;
    }

    public void RemoveTransactionLog(TransactionLog transactionLog)
    {
        _transactions.Remove(transactionLog);
    }

    public AccountType GetAccountType()
    {
        return _accountType;
    }
}