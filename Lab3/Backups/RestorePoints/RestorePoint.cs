using Backups.BackupObjects;
using Backups.Storages;

namespace Backups.RestorePoints;

public class RestorePoint
{
    private List<BackupObject> _backupObjects;

    public RestorePoint(IStorage storage, List<BackupObject> backupObjects, DateTime date)
    {
        Storage = storage;
        _backupObjects = backupObjects;
        Date = date;
    }

    public IStorage Storage { get; }

    public DateTime Date { get; }

    public int GetStorageCount()
    {
        return Storage.GetStorageCount();
    }

    public IReadOnlyList<BackupObject> GetBackupObjects()
    {
        return _backupObjects;
    }
}