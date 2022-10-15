namespace IsuExtra.Exceptions;

public class WrongLessonDayException : IsuExtraException
{
    public WrongLessonDayException(int day)
        : base($"There are no {day} day in a week ")
    {
    }
}