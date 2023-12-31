﻿using Banks.Entities.Notification;
using Banks.Entities.Transaction;
using Banks.Exceptions;

namespace Banks.Entities.BackAccount;

public class DebitAccount : IBankAccount
{
    private DateTime _closingDate;
    private double _interestRate;
    private Guid _accountId;
    private double _balance;
    private double _limit;
    private Client _client;
    private AccountType _accountType;
    private Bank _bank;
    private double _interestBank;
    private List<TransactionLog> _transactions;

    public DebitAccount(
        Bank bank,
        Client client,
        double balance,
        double interestRate,
        double limit,
        AccountType accountType)
    {
        _bank = bank;
        _client = client;
        _interestRate = interestRate;
        _closingDate = DateTime.Now.AddMonths(3);
        _accountId = Guid.NewGuid();
        _balance = balance;
        _limit = limit;
        _accountType = accountType;
        _interestBank = 0;
        _transactions = new List<TransactionLog>();
    }

    public void FillUpMoney(double moneyAmount)
    {
        _balance += moneyAmount;
    }

    public void TakeOffMoney(double moneyAmount)
    {
        if (!double.IsPositiveInfinity(_limit))
        {
            if (moneyAmount > _limit)
            {
                throw new ExtraWithdrawalLimitException(_limit);
            }
        }

        if (_balance < moneyAmount)
        {
            throw new NegativeBalanceException();
        }

        _balance -= moneyAmount;
    }

    public Guid GetAccountId()
    {
        return _accountId;
    }

    public void ChangeDebitInterest(double newDebitInterest)
    {
        _interestRate = newDebitInterest;
    }

    public void ChangeDebitLimit(double newLimit)
    {
        _limit = newLimit;
    }

    public void ChangeParameter(ChangeType changeType, double newParameter)
    {
        switch (changeType)
        {
            case ChangeType.DebitInterest:
                ChangeDebitInterest(newParameter);
                break;
            case ChangeType.CreditLimit:
                ChangeDebitLimit(newParameter);
                break;
            case ChangeType.AddMonthInterest:
                AddMonthInterest();
                break;
            case ChangeType.AddDayInterest:
                AddDailyInterest();
                break;
            default:
                throw new NonExistChangeTypeException();
        }
    }

    public double GetBalance()
    {
        return _balance;
    }

    public void AddDailyInterest()
    {
        _interestBank += _balance * ((_interestRate / 100) / 365);
    }

    public void AddMonthInterest()
    {
        _balance += _interestBank;
        _interestBank = 0;
    }

    public void AddTransactionLog(TransactionLog transactionLog)
    {
        _transactions.Add(transactionLog);
    }

    public void RemoveTransactionLog(TransactionLog transactionLog)
    {
        _transactions.Remove(transactionLog);
    }

    public AccountType GetAccountType()
    {
        return _accountType;
    }

    public List<TransactionLog> GetTransactions()
    {
        return _transactions;
    }
}