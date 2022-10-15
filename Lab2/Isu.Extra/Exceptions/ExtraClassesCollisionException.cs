namespace IsuExtra.Exceptions;

public class ExtraClassesCollisionException : IsuExtraException
{
    public ExtraClassesCollisionException(string name)
        : base($"ExtraClass {name} is already exists")
    {
    }
}