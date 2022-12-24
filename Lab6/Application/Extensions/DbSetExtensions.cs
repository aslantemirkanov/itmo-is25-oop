using Application.Exceptions.NotFound;
using Microsoft.EntityFrameworkCore;

namespace Application.Extensions;

public static class DbSetExtensions
{
    public static T GetEntity<T>(this DbSet<T> set, Guid id)
        where T : class
    {
        var entity = set.Find(new object[] { id });

        if (entity is null)
            throw EntityNotFoundException<T>.Create(id);

        return entity;
    }
}