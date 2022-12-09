using Banks.Entities;
using Banks.Entities.BackAccount;
using Banks.Entities.Notification;
using Banks.Models;
using Banks.Services;
using Timer = Banks.Entities.Timer;

namespace Banks.Console
{
    internal static class Program
    {
        private static void Main(string[] args)
        {/*
            CentralBank centralBank = CentralBank.GetInstance();
            Bank bank = new Bank.BankBuilder()
                .AddName("alfa")
                .AddCreditLimit(1000)
                .AddCreditInterestRate(10)
                .AddDebitInterestRate(3)
                .AddDepositInterestRate(new SortedDictionary<double, double>() { { 200, 1 } })
                .AddSuspiciousAccountLimit(400)
                .Build();
            centralBank.RegisterBank(bank);
            Client client = Client.Builder
                .AddFirstName("aslan")
                .AddSecondName("temirkanov")
                .AddPassport(new Passport("8317343567"))
                .AddPhoneNumber(new PhoneNumber("89969174661"))
                .Build();
            bank.AddClient(client);
            var account = bank.OpenAccount(client, 2000, AccountType.Debit);
            System.Console.WriteLine(account.GetBalance());
            var timer = new Timer();
            timer.RewindTime(60);
            System.Console.WriteLine(account.GetBalance());
            System.Console.WriteLine(centralBank.GetCurrentDate());*/
        }
    }
}