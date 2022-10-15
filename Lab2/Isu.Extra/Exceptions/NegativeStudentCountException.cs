using Isu.Extra.Services;

namespace IsuExtra.Exceptions;

public class NegativeStudentCountException : IsuExtraException
{
    public NegativeStudentCountException()
        : base("Trying to creat flow with negative student count")
    {
    }
}