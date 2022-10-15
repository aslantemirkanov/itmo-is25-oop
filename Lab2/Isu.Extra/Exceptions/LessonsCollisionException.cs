namespace IsuExtra.Exceptions;

public class LessonsCollisionException : IsuExtraException
{
    public LessonsCollisionException()
        : base($"That lesson is already exists")
    {
    }
}