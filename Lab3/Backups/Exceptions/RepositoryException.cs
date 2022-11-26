namespace Backups.Exceptions;

public class RepositoryException : Exception
{
    private RepositoryException(string message)
        : base(message)
    {
    }

    public static RepositoryException NonExistDirectoryException(string path)
    {
        return new RepositoryException($"Non exist directory with path {path}");
    }

    public static RepositoryException FilePathException(string path)
    {
        return new RepositoryException($"There are file in this path {path}");
    }
}