using Isu.Entities;

namespace Isu.Extra.Entities;

public class ExtraStudent
{
    public ExtraStudent(Student student, Schedule schedule)
    {
        Student = student;
        Schedule = schedule;
    }

    public Student Student { get; }
    public Schedule Schedule { get; }
}