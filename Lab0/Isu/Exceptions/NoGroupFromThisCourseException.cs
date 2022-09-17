using Isu.Models;

namespace Isu.Exceptions;

public class NoGroupFromThisCourseException : IsuException
{
    public NoGroupFromThisCourseException(CourseNumber courseNumber)
        : base($"There are no groups from {courseNumber.StudentCourse} course")
    {
    }
}