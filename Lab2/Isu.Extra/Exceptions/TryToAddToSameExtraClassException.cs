namespace IsuExtra.Exceptions;
public class TryToAddToSameExtraClassException : IsuExtraException
{
    public TryToAddToSameExtraClassException()
        : base($"Trying to add student to extra class, which located on his megafaculty")
    {
    }
}