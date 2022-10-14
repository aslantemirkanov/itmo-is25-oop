using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class Schedule
{
    private Schedule(IReadOnlyCollection<Lesson> lessons)
    {
        Lessons = lessons;
    }

    public static ScheduleBuilder Builder => new ScheduleBuilder();
    public IReadOnlyCollection<Lesson> Lessons { get; }

    public class ScheduleBuilder
    {
        private readonly List<Lesson> _lessons;

        public ScheduleBuilder()
        {
            _lessons = new List<Lesson>();
        }

        public void AddLesson(Lesson lesson)
        {
            _lessons.Add(lesson);
        }

        public Schedule Build()
        {
            return new Schedule(_lessons);
        }
    }
}