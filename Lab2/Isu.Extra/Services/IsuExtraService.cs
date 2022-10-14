using Isu.Extra.Entities;
using Isu.Extra.Models;
using IsuExtra.Exceptions;

namespace Isu.Extra.Services;

public class IsuExtraService
{
    public IsuExtraService()
    {
        ExtraLessons = new List<ExtraClass>();
    }

    private List<ExtraClass> ExtraLessons { get; }
}