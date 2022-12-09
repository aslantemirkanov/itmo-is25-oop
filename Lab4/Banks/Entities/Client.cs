using Banks.Entities.BackAccount;
using Banks.Entities.Notification;
using Banks.Exceptions;
using Banks.Models;

namespace Banks.Entities;

public class Client
{
    private Passport? _passport;
    private PhoneNumber? _phoneNumber;
    private Guid _clientId;
    private string _firstName;
    private string _secondName;
    private int _verificationStatus;
    private List<string> _notifications;

    private Client(
        string firstName,
        string secondName,
        Passport? passport,
        PhoneNumber? phoneNumber,
        int verificationStatus)
    {
        _clientId = Guid.NewGuid();
        _firstName = firstName;
        _secondName = secondName;
        _passport = passport;
        _phoneNumber = phoneNumber;
        _verificationStatus = verificationStatus;
        _notifications = new List<string>();
    }

    public static ClientBuilder Builder => new ClientBuilder();

    public void AddPassport(string passportSeries)
    {
        if (passportSeries == string.Empty)
        {
            throw ClientException.WrongPassportSeriesException(passportSeries);
        }

        _passport = new Passport(passportSeries);
        _verificationStatus = _phoneNumber == null ? 2 : 3;
    }

    public void AddPhoneNumber(string phoneNumber)
    {
        if (phoneNumber == string.Empty)
        {
            throw ClientException.WrongPhoneNumberException(phoneNumber);
        }

        _phoneNumber = new PhoneNumber(phoneNumber);
        _verificationStatus = _passport == null ? 1 : 3;
    }

    public void GetNotification(string notification)
    {
        _notifications.Add(notification);
    }

    public int GetVerificationStatus()
    {
        return _verificationStatus;
    }

    public PhoneNumber? GetPhoneNumber()
    {
        return _phoneNumber;
    }

    public Passport? GetPassport()
    {
        return _passport;
    }

    public string GetName()
    {
        return _firstName + " " + _secondName;
    }

    public class ClientBuilder
    {
        private Passport? _passport;
        private PhoneNumber? _phoneNumber;
        private string _firstName;
        private string _secondName;
        private int _verificationStatus;

        public ClientBuilder()
        {
            _firstName = string.Empty;
            _secondName = string.Empty;
            _passport = null;
            _phoneNumber = null;
            _verificationStatus = 0;
        }

        public ClientBuilder AddName(string firstName, string secondName)
        {
            _firstName = firstName;
            _secondName = secondName;
            return this;
        }

        public ClientBuilder AddPassport(Passport passport)
        {
            _passport = passport.GetPassport() == string.Empty ? null : passport;

            return this;
        }

        public ClientBuilder AddPhoneNumber(PhoneNumber phoneNumber)
        {
            _phoneNumber = phoneNumber.GetPhoneNumber() == string.Empty ? null : phoneNumber;

            return this;
        }

        public Client Build()
        {
            if (_passport == null && _phoneNumber != null)
            {
                _verificationStatus = 1;
            }

            if (_passport != null && _phoneNumber == null)
            {
                _verificationStatus = 2;
            }

            if (_passport != null && _phoneNumber != null)
            {
                _verificationStatus = 3;
            }

            return new Client(
                _firstName,
                _secondName,
                _passport,
                _phoneNumber,
                _verificationStatus);
        }
    }
}