using Isu.Entities;
using Isu.Extra.Models;
using IsuExtra.Exceptions;

namespace Isu.Extra.Entities;

public class Schedule
{
    private Schedule(List<Lesson> lessons)
    {
        Lessons = lessons;
    }

    public static ScheduleBuilder Builder => new ScheduleBuilder();
    public List<Lesson> Lessons { get; }
    public void AddLesson(Lesson lesson)
    {
        Lessons.Add(lesson);
    }

    public class ScheduleBuilder
    {
        private readonly List<Lesson> _lessons;

        public ScheduleBuilder()
        {
            _lessons = new List<Lesson>();
        }

        public ScheduleBuilder AddLesson(Lesson lesson)
        {
            if (Enumerable.Contains(_lessons, lesson))
            {
                throw new LessonsCollisionException();
            }

            _lessons.Add(lesson);
            return this;
        }

        public Schedule Build()
        {
            return new Schedule(_lessons);
        }
    }
}