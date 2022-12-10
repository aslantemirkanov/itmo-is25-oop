namespace Banks.Exceptions;

public class WrongPhoneNumberException : ClientException
{
    public WrongPhoneNumberException(string phoneNumber)
        : base($"Phone number {phoneNumber} is wrong")
    {
    }
}