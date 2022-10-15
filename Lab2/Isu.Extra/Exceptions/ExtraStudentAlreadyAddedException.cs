using Isu.Extra.Entities;

namespace IsuExtra.Exceptions;

public class ExtraStudentAlreadyAddedException : IsuExtraException
{
    public ExtraStudentAlreadyAddedException(ExtraStudent extraStudent)
        : base($"Student {extraStudent.Student.Name} is already added in stream ")
    {
    }
}