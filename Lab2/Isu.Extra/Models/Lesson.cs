using IsuExtra.Exceptions;

namespace Isu.Extra.Models;

public class Lesson
{
    public Lesson(Teacher teacher, Room room, int day, int classNumber)
    {
        if (day is <= 0 or > 7)
        {
            throw new WrongLessonDayException(day);
        }

        if (classNumber is <= 0 or >= 9)
        {
            throw new OutOfScheduleRangeException(classNumber);
        }

        Teacher = teacher;
        Room = room;
        Day = day;
        ClassNumber = classNumber;
    }

    public Teacher Teacher { get; }
    public Room Room { get; }
    public int Day { get; }
    public int ClassNumber { get; }
}