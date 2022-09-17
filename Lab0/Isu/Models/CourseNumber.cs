using Isu.Exceptions;

namespace Isu.Models;

public class CourseNumber
{
    public CourseNumber(int courseNumber)
    {
        if (courseNumber < 1 || courseNumber > 4)
        {
            throw new WrongCourseNumberException(courseNumber);
        }

        StudentCourse = courseNumber;
    }

    public int StudentCourse { get; }
}