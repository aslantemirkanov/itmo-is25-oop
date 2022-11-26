using Backups.RepositoryObjects;

namespace Backups.Repositories;

public interface IRepository
{
    IRepositoryObject GetRepositoryObject(string path);
    string GetPath();
    Stream GetStream(string path, string? name, int archiveCounter);
}