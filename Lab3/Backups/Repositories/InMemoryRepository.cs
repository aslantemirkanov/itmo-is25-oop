using Backups.Exceptions;
using Backups.RepositoryObjects;
using Zio;
using Zio.FileSystems;

namespace Backups.Repositories;

public class InMemoryRepository : IRepository
{
    private readonly Func<string, IReadOnlyList<IRepositoryObject>> _streamFromDirectoryFunc;
    private readonly Func<string, Stream> _streamFromFileFunc;
    private MemoryFileSystem _fileSystem;

    public InMemoryRepository(string repositoryPath, MemoryFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
        if (repositoryPath[repositoryPath.Length - 1].Equals('\\'))
        {
            repositoryPath = repositoryPath.Substring(0, repositoryPath.Length - 1);
        }

        if (_fileSystem.FileExists(repositoryPath))
        {
            throw RepositoryException.FilePathException(repositoryPath);
        }

        if (!_fileSystem.DirectoryExists(repositoryPath))
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
        if (_fileSystem.FileExists(fullPath))
        {
            IRepositoryObject repositoryObject = new RepositoryFile(fullPath, _streamFromFileFunc);
            return repositoryObject;
        }
        else if (_fileSystem.DirectoryExists(RepositoryPath + path))
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
        return _fileSystem.OpenFile(path + name + $"({archiveCounter}).zip", FileMode.OpenOrCreate, FileAccess.ReadWrite);
    }

    private Stream GetFileStream(string path)
    {
        return _fileSystem.OpenFile(path, FileMode.Open, FileAccess.ReadWrite);
    }

    private IReadOnlyList<IRepositoryObject> GetDirectoryStream(string path)
    {
        var pathDirectories = _fileSystem.EnumerateDirectories(path);
        var pathFiles = _fileSystem.EnumerateFiles(path);
        var repositoryDirectories = new List<IRepositoryObject>(
            pathDirectories.Select(copy =>
                new RepositoryFolder(copy.ToString(), _streamFromDirectoryFunc)));
        var repositoryFiles = new List<IRepositoryObject>(
            pathFiles.Select(copy => new RepositoryFile(copy.ToString(), _streamFromFileFunc)));
        return repositoryFiles.Concat(repositoryDirectories).ToList();
    }
}