using Isu.Models;

namespace Isu.Exceptions;

public class NoStudentsFromThisCourseException : IsuException
{
    public NoStudentsFromThisCourseException(CourseNumber courseNumber)
        : base($"There are no students from {courseNumber} course")
    {
    }
}