using Banks.Exceptions;

namespace Banks.Models;

public class PhoneNumber
{
    private readonly string _phoneNumber;

    public PhoneNumber(string phoneNumber)
    {
        if (!PhoneNumberValidation(phoneNumber))
        {
            throw ClientException.WrongPhoneNumberException(phoneNumber);
        }

        _phoneNumber = phoneNumber;
    }

    public string GetPhoneNumber()
    {
        return _phoneNumber;
    }

    private bool PhoneNumberValidation(string phoneNumber)
    {
        switch (phoneNumber.Length)
        {
            case 11 when !long.TryParse(phoneNumber, out long _):
            case 12 when !phoneNumber[0].Equals('+'):
            case 12 when !long.TryParse(phoneNumber.AsSpan(1), out long _):
                return false;
            default:
                return phoneNumber.Length is >= 11 and <= 12;
        }
    }
}