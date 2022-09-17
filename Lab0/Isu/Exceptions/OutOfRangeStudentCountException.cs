namespace Isu.Exceptions;

public class OutOfRangeStudentCountException : IsuException
{
    public OutOfRangeStudentCountException()
        : base("Trying to add too many students in base")
    {
    }
}