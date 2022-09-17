using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Models;

public class IdGenerator
{
    private const int MinId = 99999;
    private const int MaxId = 999999;
    private int _id;

    public IdGenerator()
    {
        _id = MinId;
    }

    public StudentId GenerateNewId()
    {
        _id++;
        if (_id is > MaxId or <= MinId)
        {
            throw new OutOfRangeStudentCountException();
        }

        var nextId = new StudentId(_id);
        return nextId;
    }
}