namespace Banks.Exceptions;

public class WrongPassportSeriesException : ClientException
{
    public WrongPassportSeriesException(string passportSeries)
        : base($"Passport series {passportSeries} is wrong")
    {
    }
}