using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class MegaFaculty
{
    public MegaFaculty(string name)
    {
        ExtraClasses = new List<ExtraClass>();
        Groups = new List<ExtraGroup>();
        Name = name;
    }

    public List<ExtraClass> ExtraClasses { get; }
    public List<ExtraGroup> Groups { get; }
    public string Name { get; }
}