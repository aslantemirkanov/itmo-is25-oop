using System.Transactions;
using Banks.Entities;
using Banks.Entities.BackAccount;
using Banks.Entities.Transaction;
using Banks.Models;
using Banks.Services;
using Xunit;
using Xunit.Abstractions;
using Timer = Banks.Entities.Timer;

namespace Banks.Test;

public class BankTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private CentralBank _centralBank = CentralBank.GetInstance();
    private Transacation _transacation = Transacation.GetInstance();

    private Bank _bank1 = new Bank.BankBuilder()
        .AddName("alfa")
        .AddCreditLimit(1000)
        .AddCreditInterestRate(10)
        .AddDebitInterestRate(3)
        .AddDepositInterestRate(new SortedDictionary<double, double>() { { 200, 1 } })
        .AddSuspiciousAccountLimit(400)
        .Build();

    private Bank _bank2 = new Bank.BankBuilder()
        .AddName("sber")
        .AddCreditLimit(1000)
        .AddCreditInterestRate(10)
        .AddDebitInterestRate(3)
        .AddDepositInterestRate(new SortedDictionary<double, double>() { { 200, 1 } })
        .AddSuspiciousAccountLimit(400)
        .Build();

    private Client _client1 = Client.Builder
        .AddFirstName("aslan")
        .AddSecondName("temirkanov")
        .AddPassport(new Passport("7777777777"))
        .AddPhoneNumber(new PhoneNumber("+79999999999"))
        .Build();

    private Client _client2 = Client.Builder
        .AddFirstName("daddy")
        .AddSecondName("petrov")
        .AddPassport(new Passport("7777777777"))
        .AddPhoneNumber(new PhoneNumber("+79999999999"))
        .Build();

    public BankTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void TransferInBank()
    {
        _centralBank.RegisterBank(_bank1);
        _bank1.AddClient(_client1);
        _bank1.AddClient(_client2);
        var account1 = _bank1.OpenAccount(_client1, 2000, AccountType.Credit);
        var account2 = _bank1.OpenAccount(_client2, 1000, AccountType.Deposit);
        _bank1.TransferTransaction(account1.GetAccountId(), account2.GetAccountId(), 2990);
        Assert.Equal(3990, account2.GetBalance());
    }

    [Fact]
    public void TransferBetweenBank()
    {
        _centralBank.RegisterBank(_bank1);
        _centralBank.RegisterBank(_bank2);
        _bank1.AddClient(_client1);
        _bank2.AddClient(_client2);
        var account1 = _bank1.OpenAccount(_client1, 2000, AccountType.Debit);
        _bank1.ReplenishmentTransaction(account1.GetAccountId(), 2000);
        var account2 = _bank2.OpenAccount(_client2, 1000, AccountType.Debit);
        _bank1.TransferTransaction(account1.GetAccountId(), account2.GetAccountId(), 100);
        Assert.Equal(1100, account2.GetBalance());
        Assert.Equal(3900, account1.GetBalance());
    }

    [Fact]
    public void UndoTransaction()
    {
        _centralBank.RegisterBank(_bank1);
        _centralBank.RegisterBank(_bank2);
        _bank1.AddClient(_client1);
        _bank2.AddClient(_client2);
        var account1 = _bank1.OpenAccount(_client1, 2000, AccountType.Debit);
        var account2 = _bank2.OpenAccount(_client2, 1000, AccountType.Debit);
        _bank1.TransferTransaction(account1.GetAccountId(), account2.GetAccountId(), 100);
        _transacation.UndoLastTransaction();
        Assert.Equal(1000, account2.GetBalance());
        Assert.Equal(2000, account1.GetBalance());
    }

    [Fact]
    public void WithdrawalTransaction()
    {
        _centralBank.RegisterBank(_bank1);
        _bank1.AddClient(_client1);
        var account1 = _bank1.OpenAccount(_client1, 2000, AccountType.Debit);
        _bank1.WithdrawalTransaction(account1.GetAccountId(), 1000);
        Assert.Equal(1000, account1.GetBalance());
    }

    [Fact]
    public void ReplenishmentTransaction()
    {
        _centralBank.RegisterBank(_bank1);
        _bank1.AddClient(_client1);
        var account1 = _bank1.OpenAccount(_client1, 2000, AccountType.Debit);
        _bank1.ReplenishmentTransaction(account1.GetAccountId(), 1000);
        Assert.Equal(3000, account1.GetBalance());
    }
}