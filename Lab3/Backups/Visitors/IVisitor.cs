using Backups.RepositoryObjects;

namespace Backups.Visitors;

public interface IVisitor
{
    void Visit(RepositoryFile repositoryObject);
    void Visit(RepositoryFolder repositoryObject);
}