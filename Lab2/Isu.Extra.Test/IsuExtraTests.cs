using Isu.Entities;
using Isu.Exceptions;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Isu.Models;
using IsuExtra.Exceptions;
using Xunit;
using Stream = Isu.Extra.Entities.Stream;

namespace Isu.Extra.Test;

public class IsuExtraTests
{
    private IsuExtraService _isuExtra = new IsuExtraService();

    [Fact]
    public void AddStudentToExtraClassFromHisMegaFaculty()
    {
        MegaFaculty fitip = new MegaFaculty('M', "fitip");

        Schedule schedule1 = Schedule.Builder
            .AddLesson(new Lesson(new Teacher("ak"), new Room(501), 1, 1))
            .AddLesson(new Lesson(new Teacher("aka"), new Room(401), 1, 2))
            .Build();

        ExtraGroup m3102 = new ExtraGroup(new Group(new GroupName("M3102")), schedule1);
        _isuExtra.AddGroup(m3102.Group.GroupName, schedule1);
        ExtraStudent boba = _isuExtra.AddStudent(m3102, "boba");
        ExtraClass matan = _isuExtra.AddExtraClass(fitip, "matan");
        Assert.Throws<TryToAddToSameExtraClassException>(() => _isuExtra.AddStudentToExtraClass(boba, fitip, matan));
    }

    [Fact]
    public void ScheduleCollisionTest()
    {
        MegaFaculty fitip = new MegaFaculty('M', "fitip");

        Schedule schedule1 = Schedule.Builder
            .AddLesson(new Lesson(new Teacher("ak"), new Room(501), 1, 1))
            .AddLesson(new Lesson(new Teacher("aka"), new Room(401), 1, 2))
            .Build();
        Schedule schedule2 = Schedule.Builder
            .AddLesson(new Lesson(new Teacher("ak"), new Room(501), 1, 1))
            .AddLesson(new Lesson(new Teacher("petya"), new Room(301), 2, 1))
            .Build();

        ExtraGroup m3102 = new ExtraGroup(new Group(new GroupName("P3102")), schedule1);
        _isuExtra.AddGroup(m3102.Group.GroupName, schedule1);
        ExtraStudent boba = _isuExtra.AddStudent(m3102, "boba");
        ExtraClass matan = _isuExtra.AddExtraClass(fitip, "matan");
        matan.AddStream(schedule2, 30);
        Assert.Throws<ScheduleCollisionException>(() => _isuExtra.AddStudentToExtraClass(boba, fitip, matan));
    }

    [Fact]
    public void AddExtraClassTest()
    {
        MegaFaculty fitip = new MegaFaculty('M', "fitip");

        Schedule schedule1 = Schedule.Builder
            .AddLesson(new Lesson(new Teacher("ak"), new Room(501), 1, 1))
            .Build();
        Schedule schedule2 = Schedule.Builder
            .AddLesson(new Lesson(new Teacher("ak"), new Room(501), 3, 1))
            .Build();
        Schedule schedule3 = Schedule.Builder
            .AddLesson(new Lesson(new Teacher("ak"), new Room(501), 1, 1))
            .AddLesson(new Lesson(new Teacher("ak"), new Room(501), 3, 1))
            .Build();

        ExtraGroup m3102 = new ExtraGroup(new Group(new GroupName("P3102")), schedule1);
        _isuExtra.AddGroup(m3102.Group.GroupName, schedule1);
        ExtraStudent boba = _isuExtra.AddStudent(m3102, "boba");
        ExtraClass matan = _isuExtra.AddExtraClass(fitip, "matan");
        matan.AddStream(schedule2, 30);
        _isuExtra.AddStudentToExtraClass(boba, fitip, matan);
        Assert.Equal(schedule3.Lessons, boba.Schedule.Lessons);
    }
}