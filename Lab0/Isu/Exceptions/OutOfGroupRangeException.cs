namespace Isu.Exceptions;

public class OutOfGroupRangeException : IsuException
{
    public OutOfGroupRangeException(int studentCount, int maxStudentCount)
        : base($"Trying to add {studentCount} students, what more than group capacity ({maxStudentCount})")
    {
    }
}