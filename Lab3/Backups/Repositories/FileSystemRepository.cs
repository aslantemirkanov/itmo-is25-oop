using Backups.Exceptions;
using Backups.RepositoryObjects;

namespace Backups.Repositories;

public class FileSystemRepository : IRepository
{
    private readonly Func<string, IReadOnlyList<IRepositoryObject>> _streamFromDirectoryFunc;
    private readonly Func<string, Stream> _streamFromFileFunc;

    public FileSystemRepository(string repositoryPath)
    {
        if (repositoryPath[repositoryPath.Length - 1].Equals('\\'))
        {
            repositoryPath = repositoryPath.Substring(0, repositoryPath.Length - 1);
        }

        if (File.Exists(repositoryPath))
        {
            throw RepositoryException.FilePathException(repositoryPath);
        }

        if (!Directory.Exists(repositoryPath))
        {
            throw RepositoryException.NonExistDirectoryException(repositoryPath);
        }

        RepositoryPath = repositoryPath;
        _streamFromFileFunc = GetFileStream;
        _streamFromDirectoryFunc = GetDirectoryStream;
    }

    public string RepositoryPath { get; }

    public IRepositoryObject GetRepositoryObject(string path)
    {
        string fullPath = RepositoryPath + path;
        if (File.Exists(fullPath))
        {
            IRepositoryObject repositoryObject = new RepositoryFile(fullPath, _streamFromFileFunc);
            return repositoryObject;
        }
        else if (Directory.Exists(RepositoryPath + path))
        {
            IRepositoryObject repositoryObject = new RepositoryFolder(fullPath, _streamFromDirectoryFunc);
            return repositoryObject;
        }
        else
        {
            throw BackupObjectException.NonExistBackupObjectException(fullPath);
        }
    }

    public string GetPath()
    {
        return RepositoryPath;
    }

    public Stream GetStream(string path, string? name, int archiveCounter)
    {
        return new FileStream(path + name + $"({archiveCounter}).zip", FileMode.OpenOrCreate, FileAccess.ReadWrite);
    }

    private Stream GetFileStream(string path)
    {
        return File.Open(path, FileMode.Open);
    }

    private IReadOnlyList<IRepositoryObject> GetDirectoryStream(string path)
    {
        var pathDirectories = Directory.GetDirectories(path);
        var pathFiles = Directory.GetFiles(path);
        var repositoryDirectories = new List<IRepositoryObject>(
            pathDirectories.Select(copy =>
                new RepositoryFolder(copy, _streamFromDirectoryFunc)));
        var repositoryFiles = new List<IRepositoryObject>(
            pathFiles.Select(copy => new RepositoryFile(copy, _streamFromFileFunc)));
        return repositoryFiles.Concat(repositoryDirectories).ToList();
    }
}