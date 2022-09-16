using Isu.Exceptions;

namespace Isu.Models;

public class GroupName
{
    private readonly string _name;

    public GroupName(string name)
    {
        name = name.ToUpper();
        if (!IsGroupNameValid(name))
        {
            throw new InvalidGroupNameException(name);
        }

        _name = name;
        CourseNumber = new CourseNumber((int)char.GetNumericValue(name[2]));
    }

    public CourseNumber CourseNumber { get; }

    public override string ToString()
    {
        return _name;
    }

    private static bool IsGroupNameValid(string name)
    {
        return IsNameLengthRight(name) &&
               IsNameCourseRight(name) &&
               IsFirstSymbolLetter(name) &&
               IsOtherSymbolsNumbers(name);
    }

    private static bool IsNameLengthRight(string name)
    {
        return name.Length == 5;
    }

    private static bool IsNameCourseRight(string name)
    {
        return name[2] >= '1' && name[2] <= '4';
    }

    private static bool IsFirstSymbolLetter(string name)
    {
        return char.IsLetter(name[0]);
    }

    private static bool IsOtherSymbolsNumbers(string name)
    {
        for (int i = 1; i < 5; ++i)
        {
            if (!char.IsNumber(name[i]))
            {
                return false;
            }
        }

        return true;
    }
}