namespace Isu.Exceptions;

public class WrongGroupTransferToCurrentGroupException : IsuException
{
    public WrongGroupTransferToCurrentGroupException()
        : base("Trying to add a student to the current group")
    {
    }
}