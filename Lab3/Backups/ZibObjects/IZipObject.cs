using System.IO.Compression;
using Backups.RepositoryObjects;

namespace Backups.ZibObjects;

public interface IZipObject
{
    IRepositoryObject GetRepositoryObject(ZipArchiveEntry zipArchiveEntry);
    string GetName();
}