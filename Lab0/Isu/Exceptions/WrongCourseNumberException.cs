namespace Isu.Exceptions;

public class WrongCourseNumberException : IsuException
{
    public WrongCourseNumberException(int courseNumber)
        : base($"Course have to be between 1 and 4, but get {courseNumber}")
    {
    }
}