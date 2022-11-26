using Backups.RepositoryObjects;

namespace Backups.Storages;

public interface IStorage
{
    List<IRepositoryObject> GetRepositoryObjects();
    int GetStorageCount();
}