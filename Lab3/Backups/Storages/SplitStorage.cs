using Backups.RepositoryObjects;

namespace Backups.Storages;

public class SplitStorage : IStorage
{
    private List<IStorage> _listSingleStorages;

    public SplitStorage(List<IStorage> singleStorages)
    {
        _listSingleStorages = singleStorages;
    }

    public List<IRepositoryObject> GetRepositoryObjects()
    {
        var repositoryObjects = new List<IRepositoryObject>();
        foreach (var singleStorage in _listSingleStorages)
        {
            repositoryObjects.Concat(singleStorage.GetRepositoryObjects());
        }

        return repositoryObjects;
    }

    public int GetStorageCount()
    {
        return _listSingleStorages.Count;
    }
}