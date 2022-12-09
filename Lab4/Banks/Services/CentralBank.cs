using Banks.Entities;
using Banks.Entities.BackAccount;
using Banks.Entities.Transaction;
using Banks.Exceptions;

namespace Banks.Services;

public class CentralBank
{
    private static CentralBank? _instance;
    private List<Bank> _banks;
    private DateTime _dateTime;

    private CentralBank()
    {
        _banks = new List<Bank>();
        _dateTime = DateTime.Now;
    }

    public static CentralBank GetInstance()
    {
        return _instance ??= new CentralBank();
    }

    public void TransferTransaction(Guid accountFromId, Guid accountToId, double money)
    {
        IBankAccount accountFrom = GetBankAccount(accountFromId);
        IBankAccount accountTo = GetBankAccount(accountToId);
        Transacation.GetInstance().ExecuteTransaction(TransactionType.Transfer, money, accountFrom, accountTo);
    }

    public Bank RegisterBank(
        string bankName,
        double debitInterestRate,
        SortedDictionary<double, double> depositInterestRates,
        double creditInterestRate,
        double creditLimit,
        double suspiciousAccountLimit)
    {
        Bank bank = new Bank.BankBuilder()
            .AddName(bankName)
            .AddDebitInterestRate(debitInterestRate)
            .AddDepositInterestRate(depositInterestRates)
            .AddCreditLimit(creditLimit)
            .AddCreditInterestRate(creditInterestRate)
            .AddSuspiciousAccountLimit(suspiciousAccountLimit)
            .Build();
        _banks.Add(bank);
        return bank;
    }

    public void RegisterBank(Bank bank)
    {
        _banks.Add(bank);
    }

    public void AddMonthInterest()
    {
        _banks.ForEach(u => u.AddMonthInterest());
    }

    public void AddDayInterest()
    {
        _banks.ForEach(u => u.AddDayInterest());
        _dateTime = _dateTime.AddDays(1);
    }

    public DateTime GetCurrentDate()
    {
        return _dateTime;
    }

    private IBankAccount GetBankAccount(Guid accountId)
    {
        IBankAccount? newBankAccount = null;
        foreach (var bank in _banks)
        {
            IBankAccount? foundBankAccount = bank.GetBankAccount(accountId);
            if (newBankAccount == null && foundBankAccount != null)
            {
                newBankAccount = foundBankAccount;
                return newBankAccount;
            }
        }

        throw BankException.WrongAccountId(accountId);
    }
}