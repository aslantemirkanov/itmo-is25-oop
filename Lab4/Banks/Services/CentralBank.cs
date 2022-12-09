using Banks.Entities;
using Banks.Entities.BackAccount;
using Banks.Entities.Transaction;
using Banks.Exceptions;

namespace Banks.Services;

public class CentralBank
{
    private static CentralBank? _instance;
    private List<Bank> _banks;

    private CentralBank()
    {
        _banks = new List<Bank>();
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

    public void AddInterest()
    {
        _banks.ForEach(u => u.AddInterest());
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