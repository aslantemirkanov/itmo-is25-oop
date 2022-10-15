using Isu.Extra.Models;
using IsuExtra.Exceptions;

namespace Isu.Extra.Entities;

public class MegaFaculty
{
    public MegaFaculty(char letter, string name)
    {
        ExtraClasses = new List<ExtraClass>();
        Groups = new List<ExtraGroup>();
        Name = name;
        Letter = letter;
    }

    public List<ExtraClass> ExtraClasses { get; }
    public List<ExtraGroup> Groups { get; }
    public string Name { get; }
    public char Letter { get; }

    public ExtraClass AddExtraClass(string name)
    {
        var newExtraClass = new ExtraClass(name);
        if (ExtraClasses.Any(t => newExtraClass.Name.Equals(t.Name)))
        {
            throw new ExtraClassesCollisionException(name);
        }

        ExtraClasses.Add(newExtraClass);
        return newExtraClass;
    }
}