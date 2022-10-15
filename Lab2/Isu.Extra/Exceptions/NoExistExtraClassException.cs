using Isu.Extra.Entities;

namespace IsuExtra.Exceptions;

public class NoExistExtraClassException : IsuExtraException
{
    public NoExistExtraClassException(ExtraClass extraClass)
        : base($"Extra Class {extraClass.Name} does not exist ")
    {
    }
}