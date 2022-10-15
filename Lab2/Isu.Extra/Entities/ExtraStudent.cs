using Isu.Entities;

namespace Isu.Extra.Entities;

public class ExtraStudent
{
    public ExtraStudent(Student student, Schedule schedule)
    {
        Student = student;
        Schedule = schedule;
        IsRegistered = false;
    }

    public Student Student { get; }
    public Schedule Schedule { get; set; }
    public bool IsRegistered { get; private set; }

    public void RegisterToStream(Schedule schedule)
    {
        foreach (var lesson in schedule.Lessons)
        {
            Schedule.AddLesson(lesson);
        }

        IsRegistered = true;
    }

    public void DeleteFromStream()
    {
        IsRegistered = false;
    }
}