using Backups.Exceptions;
using Backups.Repositories;
using Backups.RepositoryObjects;

namespace Backups.BackupObjects;

public class BackupObject
{
    private string _path;

    private IRepository _repository;

    public BackupObject(string path, IRepository repository)
    {
        if (!path[0].Equals('\\'))
        {
            path = '\\' + path;
        }

        _repository = repository;
        _path = path;
    }

    public string GetPath()
    {
        return _repository.GetPath() + _path;
    }

    public string GetName()
    {
        return Path.GetFileName(_repository.GetPath() + _path);
    }

    public IRepositoryObject GetRepositoryObject()
    {
        return _repository.GetRepositoryObject(_path);
    }

    public IRepository GetRepository()
    {
        return _repository;
    }
}