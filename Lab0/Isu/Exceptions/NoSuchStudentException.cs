namespace Isu.Exceptions;

public class NoSuchStudentException : IsuException
{
    public NoSuchStudentException()
        : base("Trying to get a non-existent student")
    {
    }
}