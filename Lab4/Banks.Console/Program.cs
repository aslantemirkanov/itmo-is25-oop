using System;
using System.Data;
using Banks.Entities;
using Banks.Entities.BackAccount;
using Banks.Entities.Notification;
using Banks.Entities.Transaction;
using Banks.Exceptions;
using Banks.Models;
using Banks.Services;
using Timer = Banks.Entities.Timer;

namespace Banks.Console
{
    internal static class Program
    {
        private static void WelcomeMenu()
        {
            System.Console.WriteLine("Welcome to bank system!");
            System.Console.WriteLine("1 - Create bank");
            System.Console.WriteLine("2 - Show banks");
            System.Console.WriteLine("3 - Add monthly interest");
            System.Console.WriteLine("4 - Chose concrete bank");
            System.Console.WriteLine("5 - Open bank menu");
            System.Console.WriteLine("6 - Enter the number of days to speed up");
            System.Console.WriteLine("7 - Exit");
        }

        private static void Main(string[] args)
        {
            bool isFinished = false;
            var centralBank = CentralBank.GetInstance();
            Client aslan = new Client
                    .ClientBuilder()
                .AddFirstName("aslan")
                .AddSecondName("temirkan")
                .AddPassport(new Passport("1111111111"))
                .AddPhoneNumber(new PhoneNumber("89969174661"))
                .Build();
            Client daddy = new Client
                    .ClientBuilder()
                .AddFirstName("daddy")
                .AddSecondName("dad")
                .AddPassport(new Passport("2222222222"))
                .AddPhoneNumber(new PhoneNumber("89969174661"))
                .Build();
            Bank alfaBank = new Bank.BankBuilder()
                .AddName("Alfa-Bank")
                .AddCreditLimit(1000)
                .AddCreditInterestRate(10)
                .AddDebitInterestRate(3)
                .AddDepositInterestRate(new SortedDictionary<double, double>() { { 200, 1 }, { 400, 2 }, { 600, 3 } })
                .AddSuspiciousAccountLimit(400)
                .Build();
            alfaBank.AddClient(aslan);
            var aslanAcc = alfaBank.OpenAccount(aslan, 1000, AccountType.Debit);
            System.Console.WriteLine(aslanAcc.GetAccountId());
            centralBank.RegisterBank(alfaBank);
            alfaBank.ReplenishmentTransaction(aslanAcc.GetAccountId(), 1000);
            Transacation.GetInstance().Undo(aslanAcc.GetTransactions().Last().TransactionId);
            Bank tinkoffBank = new Bank.BankBuilder()
                .AddName("Tinkoff-Bank")
                .AddCreditLimit(2000)
                .AddCreditInterestRate(20)
                .AddDebitInterestRate(5)
                .AddDepositInterestRate(new SortedDictionary<double, double>() { { 100, 2 }, { 200, 4 }, { 300, 6 } })
                .AddSuspiciousAccountLimit(400)
                .Build();
            tinkoffBank.AddClient(daddy);
            var daddyAcc = tinkoffBank.OpenAccount(daddy, 2000, AccountType.Debit);
            System.Console.WriteLine(daddyAcc.GetAccountId());
            centralBank.RegisterBank(tinkoffBank);
            Bank? currentBank = null;
            while (!isFinished)
            {
                System.Console.WriteLine("\n");
                WelcomeMenu();
                string? inputCommand = System.Console.ReadLine();
                switch (inputCommand)
                {
                    case "1":
                        var bankBuilder = new Bank.BankBuilder();
                        System.Console.WriteLine("Input bank name:");
                        bankBuilder.AddName(System.Console.ReadLine() ?? throw new Exception());
                        System.Console.WriteLine("Input Debit Interest Rate");
                        bankBuilder.AddDebitInterestRate(Convert.ToDouble(System.Console.ReadLine() ??
                                                                          throw new Exception()));
                        System.Console.WriteLine("Input Credit Interest Rate");
                        bankBuilder.AddCreditInterestRate(Convert.ToDouble(System.Console.ReadLine() ??
                                                                           throw new Exception()));
                        System.Console.WriteLine("Input Credit Limit");
                        bankBuilder.AddCreditLimit(Convert.ToDouble(System.Console.ReadLine() ??
                                                                    throw new Exception()));
                        System.Console.WriteLine("Input Suspicious Account Limit");
                        bankBuilder.AddSuspiciousAccountLimit(Convert.ToDouble(System.Console.ReadLine() ??
                                                                               throw new Exception()));
                        var deposit = new SortedDictionary<double, double>();
                        while (true)
                        {
                            System.Console.WriteLine(">Input minimal value for deposit rate:");
                            double minValue = Convert.ToDouble(System.Console.ReadLine() ?? throw new Exception());
                            System.Console.WriteLine(">Input interest rate for this minimal value:");
                            double interestRate = Convert.ToDouble(System.Console.ReadLine() ?? throw new Exception());
                            deposit.Add(minValue, interestRate);
                            System.Console.WriteLine(">One more Rate? [y/n]");
                            var cont = System.Console.ReadLine() ?? throw new Exception();
                            if (cont == "n")
                            {
                                break;
                            }
                        }

                        bankBuilder.AddDepositInterestRate(deposit);
                        Bank newBank = bankBuilder.Build();
                        centralBank.RegisterBank(newBank);
                        System.Console.WriteLine(
                            $"You created bank {newBank.GetName()} with Id = {newBank.GetBankId()}");

                        break;
                    case "2":
                        foreach (var bank in centralBank.GetBanksList())
                        {
                            System.Console.WriteLine($"Name: {bank.GetName()}\nId: {bank.GetBankId()}");
                        }

                        break;
                    case "3":
                        centralBank.AddMonthInterest();
                        System.Console.WriteLine("Interest payed");
                        break;
                    case "4":
                        System.Console.WriteLine("Input bank Id to chose it");
                        var id = System.Console.ReadLine() ?? throw new Exception();
                        foreach (var bank in centralBank.GetBanksList())
                        {
                            if (bank.GetBankId().ToString() == id)
                            {
                                currentBank = bank;
                            }
                        }

                        if (currentBank == null)
                        {
                            System.Console.WriteLine("You didn't chose bank");
                            break;
                        }

                        break;
                    case "5":
                        if (currentBank == null)
                        {
                            System.Console.WriteLine("You didn't chose bank");
                            break;
                        }

                        ConcreteBankMenu(currentBank);
                        break;
                    case "6":
                        int dayCount = Convert.ToInt32(System.Console.ReadLine() ?? throw new Exception());
                        var timer = new Timer();
                        timer.RewindTime(dayCount);
                        break;
                    case "7":
                        isFinished = true;
                        break;
                }
            }
        }

        private static void WelcomeConcreteBankMenu(string bankName)
        {
            System.Console.WriteLine($"Welcome to bank {bankName} system!");
            System.Console.WriteLine("1 - Create client");
            System.Console.WriteLine("2 - Chose client");
            System.Console.WriteLine("3 - Open current client Menu");
            System.Console.WriteLine("4 - Show all clients of this bank");
            System.Console.WriteLine("5 - Change Credit Interest");
            System.Console.WriteLine("6 - Change Credit Limit");
            System.Console.WriteLine("7 - Change Debit Interest");
            System.Console.WriteLine("8 - Exit");
        }

        private static void ConcreteBankMenu(Bank bank)
        {
            Client? currentClient = null;
            bool isFinished = false;
            while (!isFinished)
            {
                System.Console.WriteLine("\n");
                WelcomeConcreteBankMenu(bank.GetName());
                string? inputCommand = System.Console.ReadLine();
                switch (inputCommand)
                {
                    case "1":
                        var builder = new Client.ClientBuilder();
                        System.Console.WriteLine("Input client first name:");
                        builder.AddFirstName(System.Console.ReadLine() ?? throw new Exception());
                        System.Console.WriteLine("Input client second name:");
                        builder.AddSecondName(System.Console.ReadLine() ?? throw new Exception());
                        System.Console.WriteLine("Do you want to add passport? y/n");
                        var answer = System.Console.ReadLine() ?? throw new Exception();
                        if (answer == "y")
                        {
                            bool isPassportAdded = false;
                            while (!isPassportAdded)
                            {
                                System.Console.WriteLine("Input passport number:");
                                string pas = System.Console.ReadLine() ?? throw new Exception();
                                try
                                {
                                    builder.AddPassport(new Passport(pas));
                                    isPassportAdded = true;
                                }
                                catch (WrongPassportSeriesException a)
                                {
                                    System.Console.WriteLine(a.Message);
                                }
                            }
                        }

                        System.Console.WriteLine("Do you want to add phone number? y/n");
                        answer = System.Console.ReadLine() ?? throw new Exception();
                        if (answer == "y")
                        {
                            bool isPhoneAdded = false;
                            while (!isPhoneAdded)
                            {
                                System.Console.WriteLine("Input phone number:");
                                string phone = System.Console.ReadLine() ?? throw new Exception();
                                try
                                {
                                    builder.AddPhoneNumber(new PhoneNumber(phone));
                                    isPhoneAdded = true;
                                }
                                catch (WrongPhoneNumberException a)
                                {
                                    System.Console.WriteLine(a.Message);
                                }
                            }
                        }

                        Client newClient = builder.Build();
                        bank.AddClient(newClient);
                        System.Console.WriteLine(
                            $"You created client {newClient.GetName()} with Id {newClient.GetId()}");
                        break;
                    case "2":
                        System.Console.WriteLine("Input client Id to chose it");
                        var id = System.Console.ReadLine() ?? throw new Exception();
                        var foundClient = bank.GetClient(id);

                        if (foundClient == null)
                        {
                            System.Console.WriteLine("You enter wrong client id");
                            break;
                        }

                        currentClient = foundClient;
                        break;
                    case "3":
                        if (currentClient == null)
                        {
                            System.Console.WriteLine("You didn't chose client");
                            break;
                        }

                        ConcreteClientMenu(bank, currentClient);
                        break;
                    case "4":
                        System.Console.WriteLine($"All clients of bank {bank.GetName()}:");
                        foreach (var client in bank.GetBankAllClients())
                        {
                            System.Console.WriteLine($"Name: {client.GetName()} ; Id: {client.GetId()} ");
                        }

                        break;
                    case "5":
                        System.Console.WriteLine("Input new credit interest");
                        double newCreditInterest = Convert.ToDouble(System.Console.ReadLine() ?? throw new Exception());
                        bank.ChangeCreditInterest(newCreditInterest);
                        break;
                    case "6":
                        System.Console.WriteLine("Input new credit limit");
                        double newCreditLimit = Convert.ToDouble(System.Console.ReadLine() ?? throw new Exception());
                        bank.ChangeCreditLimit(newCreditLimit);
                        break;
                    case "7":
                        System.Console.WriteLine("Input new debit interest");
                        double newDebitInterest = Convert.ToDouble(System.Console.ReadLine() ?? throw new Exception());
                        bank.ChangeDebitInterest(newDebitInterest);
                        break;
                    case "8":
                        isFinished = true;
                        break;
                }
            }
        }

        private static void WelcomeConcreteClientMenu(string clientName)
        {
            System.Console.WriteLine($"Hello, {clientName}!");
            System.Console.WriteLine("1 - Add passport");
            System.Console.WriteLine("2 - Add phone number");
            System.Console.WriteLine("3 - Create account");
            System.Console.WriteLine("4 - Subscribe for phone notification");
            System.Console.WriteLine("5 - Make transaction");
            System.Console.WriteLine("6 - Undo transaction");
            System.Console.WriteLine("7 - Show all info about clients accounts");
            System.Console.WriteLine("8 - Show account transactions Id");
            System.Console.WriteLine("9 - Show notifications");
            System.Console.WriteLine("10 - Show client's info");
            System.Console.WriteLine("11 - Exit");
        }

        private static void ConcreteClientMenu(Bank bank, Client client)
        {
            /*IBankAccount? currentAccount = null;*/
            bool isFinished = false;
            while (!isFinished)
            {
                System.Console.WriteLine("\n");
                WelcomeConcreteClientMenu(client.GetName());
                string? inputCommand = System.Console.ReadLine();
                switch (inputCommand)
                {
                    case "1":
                        System.Console.WriteLine("Input new passport number");
                        var passport = System.Console.ReadLine() ?? throw new Exception();
                        try
                        {
                            bank.AddClientPassport(client, passport);
                            System.Console.WriteLine($"You added passport {passport}");
                        }
                        catch (WrongPassportSeriesException a)
                        {
                            System.Console.WriteLine(a.Message);
                        }

                        break;
                    case "2":
                        System.Console.WriteLine("Input new phone number");
                        var phone = System.Console.ReadLine() ?? throw new Exception();
                        try
                        {
                            bank.AddClientPhoneNumber(client, phone);
                            System.Console.WriteLine($"You added phone {phone}");
                        }
                        catch (WrongPhoneNumberException a)
                        {
                            System.Console.WriteLine(a.Message);
                        }

                        break;
                    case "3":
                        System.Console.WriteLine("Input balance:");
                        double balance = Convert.ToDouble(System.Console.ReadLine() ?? throw new Exception());
                        System.Console.WriteLine("1 - Debit account");
                        System.Console.WriteLine("2 - Deposit account");
                        System.Console.WriteLine("3 - Credit account");
                        var res = System.Console.ReadLine() ?? throw new Exception();
                        IBankAccount? bankAccount = null;
                        switch (res)
                        {
                            case "1":
                                try
                                {
                                    bankAccount = bank.OpenAccount(client, balance, AccountType.Debit);
                                }
                                catch (NegativeBalanceException b)
                                {
                                    System.Console.WriteLine(b.Message);
                                }

                                break;
                            case "2":
                                try
                                {
                                    bankAccount = bank.OpenAccount(client, balance, AccountType.Deposit);
                                }
                                catch (NegativeBalanceException b)
                                {
                                    System.Console.WriteLine(b.Message);
                                }
                                catch (WrongDepositBalanceException a)
                                {
                                    System.Console.WriteLine(a.Message);
                                }

                                break;
                            case "3":
                                try
                                {
                                    bankAccount = bank.OpenAccount(client, balance, AccountType.Credit);
                                }
                                catch (TryToCreateVerificatedAccountException a)
                                {
                                    System.Console.WriteLine(a.Message);
                                }
                                catch (NegativeBalanceException b)
                                {
                                    System.Console.WriteLine(b.Message);
                                }

                                break;
                        }

                        System.Console.Write(
                            $"You opened {bankAccount?.GetAccountType().ToString()} account ");
                        System.Console.WriteLine("with Id = {bankAccount?.GetAccountId()}");
                        break;
                    case "4":
                        try
                        {
                            bank.AddPhoneNumberSubscriber(client);
                            System.Console.WriteLine("You subscribed for phone notifications");
                        }
                        catch (TryToGetNullPhoneNumberException a)
                        {
                            System.Console.WriteLine(a.Message);
                        }

                        break;
                    case "5":
                        System.Console.WriteLine("1 - Transfer");
                        System.Console.WriteLine("2 - Replenishment");
                        System.Console.WriteLine("3 - Withdrawal");
                        var ans = System.Console.ReadLine() ?? throw new Exception();
                        switch (ans)
                        {
                            case "1":
                                System.Console.WriteLine("Input the sender's account id");
                                Guid accountFrom = Guid.Parse(System.Console.ReadLine() ?? throw new Exception());
                                System.Console.WriteLine("Input the reciever's account id");
                                Guid accountTo = Guid.Parse(System.Console.ReadLine() ?? throw new Exception());
                                System.Console.WriteLine("Input the transfer amount");
                                var moneyTransfer =
                                    Convert.ToDouble(System.Console.ReadLine() ?? throw new Exception());
                                try
                                {
                                    bank.TransferTransaction(accountFrom, accountTo, moneyTransfer);
                                    System.Console.WriteLine($"You transferred {moneyTransfer} USD");
                                }
                                catch (ExtraWithdrawalLimitException a)
                                {
                                    System.Console.WriteLine(a.Message);
                                }
                                catch (NegativeBalanceException a)
                                {
                                    System.Console.WriteLine(a.Message);
                                }
                                catch (WrongAccountIdException a)
                                {
                                    System.Console.WriteLine(a.Message);
                                }
                                catch (CreditLimitExcessException a)
                                {
                                    System.Console.WriteLine(a.Message);
                                }

                                break;
                            case "2":
                                System.Console.WriteLine("Input the account id");
                                Guid accountId = Guid.Parse(System.Console.ReadLine() ?? throw new Exception());
                                System.Console.WriteLine("Input the replenishment amount");
                                var moneyReplenishment =
                                    Convert.ToDouble(System.Console.ReadLine() ?? throw new Exception());
                                try
                                {
                                    bank.ReplenishmentTransaction(accountId, moneyReplenishment);
                                    System.Console.WriteLine($"You filled up for {moneyReplenishment} USD");
                                }
                                catch (ExtraWithdrawalLimitException a)
                                {
                                    System.Console.WriteLine(a.Message);
                                }
                                catch (NegativeBalanceException a)
                                {
                                    System.Console.WriteLine(a.Message);
                                }
                                catch (WrongAccountIdException a)
                                {
                                    System.Console.WriteLine(a.Message);
                                }
                                catch (CreditLimitExcessException a)
                                {
                                    System.Console.WriteLine(a.Message);
                                }

                                break;
                            case "3":
                                System.Console.WriteLine("Input the account id");
                                Guid accId = Guid.Parse(System.Console.ReadLine() ?? throw new Exception());
                                System.Console.WriteLine("Input the withdrawal amount");
                                var moneyWithdrawal =
                                    Convert.ToDouble(System.Console.ReadLine() ?? throw new Exception());
                                try
                                {
                                    bank.WithdrawalTransaction(accId, moneyWithdrawal);
                                    System.Console.WriteLine($"You took off {moneyWithdrawal} USD");
                                }
                                catch (ExtraWithdrawalLimitException a)
                                {
                                    System.Console.WriteLine(a.Message);
                                }
                                catch (NegativeBalanceException a)
                                {
                                    System.Console.WriteLine(a.Message);
                                }
                                catch (WrongAccountIdException a)
                                {
                                    System.Console.WriteLine(a.Message);
                                }
                                catch (CreditLimitExcessException a)
                                {
                                    System.Console.WriteLine(a.Message);
                                }

                                break;
                        }

                        break;
                    case "6":
                        System.Console.WriteLine("Input transaction id");
                        Guid transactionId = Guid.Parse(System.Console.ReadLine() ?? throw new Exception());
                        try
                        {
                            Transacation.GetInstance().Undo(transactionId);
                        }
                        catch (NonExistTransactionException a)
                        {
                            System.Console.WriteLine(a.Message);
                        }
                        catch (ExtraWithdrawalLimitException a)
                        {
                            System.Console.WriteLine(a.Message);
                        }
                        catch (NegativeBalanceException a)
                        {
                            System.Console.WriteLine(a.Message);
                        }
                        catch (WrongAccountIdException a)
                        {
                            System.Console.WriteLine(a.Message);
                        }
                        catch (CreditLimitExcessException a)
                        {
                            System.Console.WriteLine(a.Message);
                        }

                        break;
                    case "7":
                        foreach (var bankAcc in bank.GetClientAccounts(client))
                        {
                            System.Console.WriteLine(
                                $"Id = {bankAcc.GetAccountId()}  balance = {bankAcc.GetBalance()}");
                        }

                        break;
                    case "8":
                        System.Console.WriteLine("Input account Id");
                        Guid accIdForTransactions = Guid.Parse(System.Console.ReadLine() ?? throw new Exception());
                        IBankAccount? account = bank.GetBankAccount(accIdForTransactions);
                        if (account == null)
                        {
                            System.Console.WriteLine("Client don't have that account");
                        }
                        else
                        {
                            List<TransactionLog> transactions = account.GetTransactions();
                            foreach (var transactionLog in transactions)
                            {
                                System.Console.WriteLine(
                                    $"Id = {transactionLog.TransactionId} type = {transactionLog.TransactionType}");
                            }
                        }

                        break;
                    case "9":
                        List<string> notifications = client.ShowNotifications();
                        foreach (var notification in notifications)
                        {
                            System.Console.WriteLine(notification);
                        }

                        break;
                    case "10":
                        System.Console.WriteLine($"Name {client.GetName()}");
                        System.Console.WriteLine($"Id {client.GetId()}");
                        System.Console.WriteLine($"Passport {client.GetPassport()?.GetPassport()}");
                        System.Console.WriteLine($"Phone number {client.GetPhoneNumber()?.GetPhoneNumber()}");
                        System.Console.WriteLine($"Verification status {client.GetVerificationStatus()}");
                        break;
                    case "11":
                        isFinished = true;
                        break;
                }
            }
        }
    }
}