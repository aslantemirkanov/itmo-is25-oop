using Backups.Visitors;

namespace Backups.RepositoryObjects;

public interface IRepositoryObject
{
    void Accept(IVisitor visitor);
}