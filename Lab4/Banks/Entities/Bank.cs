using Banks.Entities.BackAccount;
using Banks.Entities.Notification;
using Banks.Entities.Transaction;
using Banks.Exceptions;
using Banks.Models;
using Banks.Services;

namespace Banks.Entities;

public class Bank
{
    private Guid _bankId;
    private string _bankName;
    private Dictionary<Client, List<IBankAccount>> _clients;
    private Dictionary<Client, INotification> _subscribers;
    private List<IBankAccount> _bankAccounts;
    private double _debitInterestRate;
    private double _creditInterestRate;
    private double _creditLimit;
    private double _suspiciousAccountLimit;
    private SortedDictionary<double, double> _depositInterestRates;

    private Bank(
        string bankName,
        double debitInterestRate,
        SortedDictionary<double, double> depositInterestRates,
        double creditInterestRate,
        double creditLimit,
        double suspiciousAccountLimit)
    {
        _bankId = Guid.NewGuid();
        _bankName = bankName;
        _debitInterestRate = debitInterestRate;
        _depositInterestRates = depositInterestRates;
        _creditInterestRate = creditInterestRate;
        _creditLimit = creditLimit;
        _suspiciousAccountLimit = suspiciousAccountLimit;
        _clients = new Dictionary<Client, List<IBankAccount>>();
        _subscribers = new Dictionary<Client, INotification>();
        _bankAccounts = new List<IBankAccount>();
    }

    public static BankBuilder Builder => new BankBuilder();

    public IBankAccount OpenAccount(Client client, double balance, AccountType accountType)
    {
        double limit = double.PositiveInfinity;
        if (client.GetVerificationStatus() != 3)
        {
            if (accountType == AccountType.Credit)
            {
                throw new TryToCreateVerificatedAccountException();
            }

            limit = _suspiciousAccountLimit;
        }

        if (balance < 0)
        {
            throw new NegativeBalanceException();
        }

        IBankAccount bankAccount = accountType switch
        {
            AccountType.Debit => new DebitAccount(
                this,
                client,
                balance,
                _debitInterestRate,
                limit,
                AccountType.Debit),
            AccountType.Credit => new CreditAccount(
                this,
                client,
                _creditInterestRate,
                _creditLimit,
                balance,
                AccountType.Deposit),
            AccountType.Deposit => new DepositAccount(
                this,
                client,
                GetDepositInterestRate(balance),
                balance,
                AccountType.Credit),
            _ => throw new NonExistAccountTypeException()
        };

        _clients[client].Add(bankAccount);
        _bankAccounts.Add(bankAccount);
        client.GetNotification($"You opened {accountType} account");
        return bankAccount;
    }

    public Client AddClient(
        string firstName,
        string secondName,
        Passport? passport = null,
        PhoneNumber? phoneNumber = null)
    {
        Client client = new Client.ClientBuilder()
            .AddFirstName(firstName)
            .AddSecondName(secondName)
            .AddPassport(passport ?? new Passport(string.Empty))
            .AddPhoneNumber(phoneNumber ?? new PhoneNumber(string.Empty))
            .Build();

        _clients.Add(client, new List<IBankAccount>());
        return client;
    }

    public void AddClient(Client client)
    {
        _clients.Add(client, new List<IBankAccount>());
    }

    public void AddClientPassport(Client client, string passport)
    {
        client.AddPassport(passport);
        if (client.GetVerificationStatus() == 3)
        {
            foreach (IBankAccount bankAccount in _clients[client]
                         .Where(bankAccount => bankAccount.GetAccountType().Equals(AccountType.Debit)))
            {
                bankAccount.ChangeParameter(ChangeType.CreditLimit, double.PositiveInfinity);
            }
        }
    }

    public void AddClientPhoneNumber(Client client, string phoneNumber)
    {
        client.AddPhoneNumber(phoneNumber);
        if (client.GetVerificationStatus() == 3)
        {
            foreach (IBankAccount bankAccount in _clients[client]
                         .Where(bankAccount => bankAccount.GetAccountType().Equals(AccountType.Debit)))
            {
                bankAccount.ChangeParameter(ChangeType.CreditLimit, double.PositiveInfinity);
            }
        }
    }

    public void AddPhoneNumberSubscriber(Client client)
    {
        if (client.GetPhoneNumber() == null)
        {
            throw new TryToGetNullPhoneNumberException(client);
        }

        if (!_subscribers.ContainsKey(client))
        {
            _subscribers.Add(client, new PhoneNotification());
        }
    }

    public void AddMailSubscriber(Client client)
    {
        _subscribers[client] = new MailNotification(_subscribers[client]);
    }

    public void SendNotification(string message, AccountType accountType)
    {
        foreach (Client client in from client in _subscribers.Keys
                 from bankAccount in _clients[client]
                 where bankAccount.GetAccountType().Equals(accountType)
                 select client)
        {
            _subscribers[client].Send(client, message);
        }
    }

    public void ChangeCreditInterest(double newCreditInterest)
    {
        _creditInterestRate = newCreditInterest;
        foreach (IBankAccount bankAccount in _bankAccounts.Where(bankAccount =>
                     bankAccount.GetAccountType().Equals(AccountType.Credit)))
        {
            bankAccount.ChangeParameter(ChangeType.CreditInterest, newCreditInterest);
        }

        SendNotification(
            $"Credit interest changed from {_creditInterestRate} to {newCreditInterest}",
            AccountType.Credit);
    }

    public void ChangeCreditLimit(double newCreditLimit)
    {
        _creditLimit = newCreditLimit;
        foreach (IBankAccount bankAccount in _bankAccounts.Where(bankAccount =>
                     bankAccount.GetAccountType().Equals(AccountType.Credit)))
        {
            bankAccount.ChangeParameter(ChangeType.CreditLimit, newCreditLimit);
        }

        SendNotification(
            $"Credit limit changed from {_creditLimit} to {newCreditLimit}",
            AccountType.Credit);
    }

    public void ChangeDebitInterest(double newDebitInterest)
    {
        _debitInterestRate = newDebitInterest;
        foreach (IBankAccount bankAccount in _bankAccounts.Where(bankAccount =>
                     bankAccount.GetAccountType().Equals(AccountType.Debit)))
        {
            bankAccount.ChangeParameter(ChangeType.DebitInterest, newDebitInterest);
        }

        SendNotification(
            $"Debit interest changed from {_debitInterestRate} to {newDebitInterest}",
            AccountType.Debit);
    }

    public void ChangeDepositInterest(SortedDictionary<double, double> newDepositInterest)
    {
        _depositInterestRates = newDepositInterest;
        foreach (IBankAccount bankAccount in _bankAccounts.Where(bankAccount =>
                     bankAccount.GetAccountType().Equals(AccountType.Deposit)))
        {
            bankAccount.ChangeParameter(
                ChangeType.DepositInterest,
                GetDepositInterestRate(bankAccount.GetBalance()));
        }

        SendNotification(
            $"Deposit interest changed",
            AccountType.Deposit);
    }

    public IBankAccount? GetBankAccount(Guid accountId)
    {
        IBankAccount? newBankAccount = null;
        foreach (IBankAccount bankAccount in _bankAccounts.Where(bankAccount =>
                     bankAccount.GetAccountId().Equals(accountId)))
        {
            newBankAccount = bankAccount;
        }

        return newBankAccount;
    }

    public void ReplenishmentTransaction(Guid accountId, double money)
    {
        IBankAccount? newBankAccount = GetBankAccount(accountId);
        if (newBankAccount == null)
        {
            throw new WrongAccountIdException(accountId);
        }

        Transacation.GetInstance().ExecuteTransaction(TransactionType.Replenishment, money, newBankAccount);
    }

    public void WithdrawalTransaction(Guid accountId, double money)
    {
        IBankAccount? newBankAccount = GetBankAccount(accountId);
        if (newBankAccount == null)
        {
            throw new WrongAccountIdException(accountId);
        }

        Transacation.GetInstance().ExecuteTransaction(TransactionType.Withdrawal, money, newBankAccount);
    }

    public void TransferTransaction(Guid accountFromId, Guid accountToId, double money)
    {
        IBankAccount? accountFrom = GetBankAccount(accountFromId);
        IBankAccount? accountTo = GetBankAccount(accountToId);
        if (accountFrom != null && accountTo != null)
        {
            Transacation.GetInstance().ExecuteTransaction(TransactionType.Transfer, money, accountFrom, accountTo);
        }
        else
        {
            CentralBank.GetInstance().TransferTransaction(accountFromId, accountToId, money);
        }
    }

    public void AddMonthInterest()
    {
        foreach (IBankAccount bankAccount in _bankAccounts.Where(bankAccount =>
                     bankAccount.GetAccountType().Equals(AccountType.Debit) ||
                     bankAccount.GetAccountType().Equals(AccountType.Deposit)))
        {
            bankAccount.ChangeParameter(ChangeType.AddMonthInterest, 0);
        }
    }

    public void AddDayInterest()
    {
        foreach (IBankAccount bankAccount in _bankAccounts.Where(bankAccount =>
                     bankAccount.GetAccountType().Equals(AccountType.Debit) ||
                     bankAccount.GetAccountType().Equals(AccountType.Deposit)))
        {
            bankAccount.ChangeParameter(ChangeType.AddDayInterest, 0);
        }
    }

    public Guid GetBankId()
    {
        return _bankId;
    }

    public Client? GetClient(string id)
    {
        return _clients.Keys.FirstOrDefault(client => client.GetId().ToString() == id);
    }

    public IReadOnlyList<Client> GetBankAllClients()
    {
        return _clients.Keys.ToList();
    }

    public IReadOnlyList<IBankAccount> GetClientAccounts(Client client)
    {
        return _clients[client];
    }

    public string GetName()
    {
        return _bankName;
    }

    private double GetDepositInterestRate(double balance)
    {
        foreach (double thresholdBalance in _depositInterestRates.Keys.Where(thresholdBalance =>
                     thresholdBalance <= balance))
        {
            return _depositInterestRates[thresholdBalance];
        }

        throw new WrongDepositBalanceException(balance);
    }

    public class BankBuilder
    {
        private Guid _bankId;
        private string _bankName;
        private Dictionary<Client, List<IBankAccount>> _clients;
        private Dictionary<Client, INotification> _subscribers;
        private List<IBankAccount> _bankAccounts;
        private double _debitInterestRate;
        private double _creditInterestRate;
        private double _creditLimit;
        private double _suspiciousAccountLimit;
        private SortedDictionary<double, double> _depositInterestRates;

        public BankBuilder()
        {
            _bankId = Guid.NewGuid();
            _bankName = string.Empty;
            _clients = new Dictionary<Client, List<IBankAccount>>();
            _subscribers = new Dictionary<Client, INotification>();
            _bankAccounts = new List<IBankAccount>();
            _depositInterestRates = new SortedDictionary<double, double>();
        }

        public BankBuilder AddName(string name)
        {
            _bankName = name;
            return this;
        }

        public BankBuilder AddDebitInterestRate(double debitInterestRate)
        {
            _debitInterestRate = debitInterestRate;
            return this;
        }

        public BankBuilder AddCreditInterestRate(double creditInterestRate)
        {
            _creditInterestRate = creditInterestRate;
            return this;
        }

        public BankBuilder AddCreditLimit(double creditLimit)
        {
            _creditLimit = creditLimit;
            return this;
        }

        public BankBuilder AddSuspiciousAccountLimit(double suspiciousAccountLimit)
        {
            _suspiciousAccountLimit = suspiciousAccountLimit;
            return this;
        }

        public BankBuilder AddDepositInterestRate(SortedDictionary<double, double> depositInterestRate)
        {
            _depositInterestRates = depositInterestRate;
            return this;
        }

        public Bank Build()
        {
            return new Bank(
                _bankName,
                _debitInterestRate,
                _depositInterestRates,
                _creditInterestRate,
                _creditLimit,
                _suspiciousAccountLimit);
        }
    }
}