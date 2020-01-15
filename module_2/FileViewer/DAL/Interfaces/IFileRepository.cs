using System;
using System.Collections.Generic;
using DAL.Enums;

namespace DAL.Interfaces
{
    public interface IFileRepository
    {
        IEnumerable<string> GetDirectories(Func<string, bool> filter = null);
        IEnumerable<string> GetFiles(Func<string, bool> filter = null);
        event Func<string, Operation> FileFinded;
        event Func<string, Operation> DirectoryFinded;
        event Func<string, Operation> FilteredFileFinded;
        event Func<string, Operation> FilteredDirectoryFinded;
    }
}

