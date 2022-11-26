using Backups.Algorithms;
using Backups.Archiver;
using Backups.BackupObjects;
using Backups.Backups;
using Backups.Exceptions;
using Backups.Repositories;
using Backups.RestorePoints;

namespace Backups.BackupTasks;

public class BackupTask : IBackupTask
{
    private List<BackupObject> _backupObjects;
    private Backup _backups;
    private IRepository _repository;
    private int _archiveCounter;

    public BackupTask(IRepository repository)
    {
        _archiveCounter = 1;
        _repository = repository;
        _backupObjects = new List<BackupObject>();
        _backups = new Backup();
    }

    public void AddBackupObject(BackupObject backupObject)
    {
        foreach (var backupObj in _backupObjects.Where(backupObj => backupObj.Equals(backupObject)))
        {
            throw BackupObjectException.TryToAddExistBackupObjectException(backupObj.GetPath());
        }

        _backupObjects.Add(backupObject);
    }

    public void RemoveBackupObject(BackupObject backupObject)
    {
        if (!_backupObjects.Remove(backupObject))
        {
            throw BackupObjectException.NonExistBackupObjectException(backupObject.GetPath());
        }
    }

    public void RunStorageAlgorithm(IStorageAlgorithm algorithm, IArchiver archiver)
    {
        var storage = algorithm.RunAlgorithm(_repository, archiver, _archiveCounter, _backupObjects.ToArray());
        var backupObjectsCopy = new List<BackupObject>(_backupObjects);
        var restorePoint = new RestorePoint(storage, backupObjectsCopy, DateTime.Now);
        _backups.AddRestorePoint(restorePoint);
        _archiveCounter++;
    }

    public int GetStorageCount()
    {
        return _backups.GetRestorePoints().Sum(restorePoint => restorePoint.GetStorageCount());
    }

    public int GetRestorePointCount()
    {
        return _backups.GetRestorePoints().Count;
    }
}