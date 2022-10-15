using IsuExtra.Exceptions;

namespace Isu.Extra.Entities;

public class Stream
{
    private readonly List<ExtraStudent> _extraStudents;

    public Stream(int streamCount, int maxStudentCount, Schedule schedule)
    {
        if (maxStudentCount < 0)
        {
            throw new NegativeStudentCountException();
        }

        _extraStudents = new List<ExtraStudent>();
        StreamCount = streamCount;
        StreamSchedule = schedule;
        MaxStudentCount = maxStudentCount;
    }

    public Schedule StreamSchedule { get; }
    public int MaxStudentCount { get; }
    public int StreamCount { get; }

    public void AddExtraStudent(ExtraStudent extraStudent)
    {
        if (Enumerable.Contains(_extraStudents, extraStudent))
        {
            throw new ExtraStudentAlreadyAddedException(extraStudent);
        }

        _extraStudents.Add(extraStudent);
    }

    public void RemoveExtraStudent(ExtraStudent extraStudent)
    {
        _extraStudents.Remove(extraStudent);
    }

    public bool IsSchedulesOverlap(ExtraStudent extraStudent)
    {
        return extraStudent.Schedule.Lessons.Where((t1, i) => (from t in StreamSchedule.Lessons
            where t1.Day == t.Day
            where t1.ClassNumber == StreamSchedule.Lessons[i].ClassNumber
            where t1.Room == StreamSchedule.Lessons[i].Room
            select t).Any(t => t1.Teacher == StreamSchedule.Lessons[i].Teacher)).Any();
    }

    public List<ExtraStudent> GetStreamExtraStudents()
    {
        return _extraStudents;
    }
}