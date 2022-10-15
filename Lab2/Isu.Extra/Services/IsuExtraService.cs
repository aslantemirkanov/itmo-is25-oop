using Isu.Entities;
using Isu.Exceptions;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Models;
using Isu.Services;
using IsuExtra.Exceptions;
using Stream = Isu.Extra.Entities.Stream;

namespace Isu.Extra.Services;

public class IsuExtraService
{
    private IsuService _isuService;
    private List<MegaFaculty> _megaFaculties;
    private List<ExtraStudent> _extraStudents;
    private List<ExtraGroup> _extraGroups;

    public IsuExtraService()
    {
        _isuService = new IsuService();
        _megaFaculties = new List<MegaFaculty>();
        _extraStudents = new List<ExtraStudent>();
        _extraGroups = new List<ExtraGroup>();
    }

    public MegaFaculty AddMegaFaculty(char letter, string name)
    {
        var megaFaculty = new MegaFaculty(letter, name);
        foreach (var mFaculty in _megaFaculties)
        {
            if (mFaculty.Name.Equals(name))
            {
                throw new IsuException("Trying to create already exist MegaFaculty");
            }
        }

        _megaFaculties.Add(megaFaculty);
        return megaFaculty;
    }

    public ExtraGroup AddGroup(GroupName groupName, Schedule schedule)
    {
        Group newGroup = _isuService.AddGroup(groupName);
        var newExtraGroup = new ExtraGroup(newGroup, schedule);
        if (Enumerable.Contains(_extraGroups, newExtraGroup))
        {
            throw new ExtraGroupCollisionException(groupName);
        }

        _extraGroups.Add(newExtraGroup);
        return newExtraGroup;
    }

    public ExtraStudent AddStudent(ExtraGroup extraGroup, string name)
    {
        Student newStudent = _isuService.AddStudent(extraGroup.Group, name);
        var newExtraStudent = new ExtraStudent(newStudent, extraGroup.Schedule);
        if (Enumerable.Contains(extraGroup.ExtraStudents, newExtraStudent))
        {
            throw new ExtraStudentAlreadyAddedException(newExtraStudent);
        }

        _extraStudents.Add(newExtraStudent);
        return newExtraStudent;
    }

    public void ChangeStudentGroup(ExtraStudent extraStudent, ExtraGroup newExtraGroup)
    {
        _isuService.ChangeStudentGroup(extraStudent.Student, newExtraGroup.Group);
        extraStudent.Schedule = newExtraGroup.Schedule;
        extraStudent.DeleteFromStream();
    }

    public ExtraClass AddExtraClass(MegaFaculty megaFaculty, string name)
    {
        return megaFaculty.AddExtraClass(name);
    }

    public void AddStreamToExtraClass(MegaFaculty megaFaculty, ExtraClass extraClass, Schedule schedule, int maxStudentCount)
    {
        foreach (ExtraClass t in megaFaculty.ExtraClasses.Where(t => extraClass.Name.Equals(t.Name)))
        {
            t.AddStream(schedule, maxStudentCount);
            return;
        }

        throw new NoExistExtraClassException(extraClass);
    }

    public void AddStudentToExtraClass(ExtraStudent extraStudent, MegaFaculty megaFaculty, ExtraClass extraClass)
    {
        foreach (ExtraClass t in megaFaculty.ExtraClasses)
        {
            bool flag = false;
            if (extraStudent.Student.Group.GroupName.ToString()[0] != megaFaculty.Letter)
            {
                if (t.Name.Equals(extraClass.Name))
                {
                    for (int i = 0; i < extraClass.Streams.Count; ++i)
                    {
                        if (!extraClass.Streams[i].IsSchedulesOverlap(extraStudent))
                        {
                            if (extraClass.Streams[i].GetStreamExtraStudents().Count <=
                                extraClass.Streams[i].MaxStudentCount && !extraStudent.IsRegistered)
                            {
                                extraClass.Streams[i].AddExtraStudent(extraStudent);
                                extraStudent.RegisterToStream(extraClass.Streams[i].StreamSchedule);
                                flag = true;
                            }
                        }
                    }
                }

                if (!flag)
                {
                    throw new ScheduleCollisionException();
                }
            }
            else
            {
                throw new TryToAddToSameExtraClassException();
            }
        }
    }

    public bool IsCorrectStream(ExtraStudent extraStudent, Stream stream)
    {
        return stream.IsSchedulesOverlap(extraStudent);
    }

    public void RemoveEntry(ExtraStudent extraStudent, MegaFaculty megaFaculty, ExtraClass extraClass)
    {
        foreach (var exClass in megaFaculty.ExtraClasses)
        {
            if (exClass.Name.Equals(extraClass.Name))
            {
                foreach (var stream in extraClass.Streams)
                {
                    foreach (var student in stream.GetStreamExtraStudents())
                    {
                        if (student.Equals(extraStudent))
                        {
                            extraStudent.DeleteFromStream();
                            stream.RemoveExtraStudent(extraStudent);
                            return;
                        }
                    }
                }
            }
        }
    }

    public List<Stream> GetStreamsByCourse(CourseNumber courseNumber)
    {
        var streamsList = new List<Stream>();
        foreach (var megaFaculty in _megaFaculties)
        {
            foreach (var extraClass in megaFaculty.ExtraClasses)
            {
                foreach (var stream in extraClass.Streams)
                {
                    if (stream.GetStreamExtraStudents()[0].Student.Group.GroupName.CourseNumber.Equals(courseNumber))
                    {
                        streamsList.Add(stream);
                    }
                }
            }
        }

        return streamsList;
    }

    public List<ExtraStudent> GetStudentsByExtraClass(ExtraClass extraClass, Stream stream)
    {
        if ((from megaFaculty in _megaFaculties
                from exClass in megaFaculty.ExtraClasses
                where exClass.Name.Equals(extraClass.Name)
                from fStream in exClass.Streams
                select fStream).Contains(stream))
        {
            return stream.GetStreamExtraStudents();
        }

        throw new IsuExtraException("Non-exist stream");
    }

    public List<ExtraStudent> StudentsWithoutRegisterByGroup(ExtraGroup extraGroup)
    {
        return extraGroup.ExtraStudents.Where(extraStudent => !extraStudent.IsRegistered).ToList();
    }
}