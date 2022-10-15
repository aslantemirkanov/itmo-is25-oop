namespace IsuExtra.Exceptions;
public class OutOfGroupCapacityException : IsuExtraException
{
    public OutOfGroupCapacityException(int maxStudentCount)
        : base($"Trying to add more than 30 (max) students in group")
    {
    }
}