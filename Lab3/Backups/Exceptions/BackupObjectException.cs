namespace Backups.Exceptions;

public class BackupObjectException : Exception
{
    private BackupObjectException(string message)
        : base(message)
    {
    }

    public static BackupObjectException NonExistFileException(string path)
    {
        return new BackupObjectException($"Non exist file with path {path}");
    }

    public static BackupObjectException NonExistDirectoryException(string path)
    {
        return new BackupObjectException($"Non exist directory with path {path}");
    }

    public static BackupObjectException NonExistBackupObjectException(string path)
    {
        return new BackupObjectException($"Non exist backup object with path {path}");
    }

    public static BackupObjectException TryToAddExistBackupObjectException(string path)
    {
        return new BackupObjectException($"Backup Object with path {path} is already exist");
    }
}