using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IFileSystemVisitor: IEnumerable<string>
    {
        event Action FilesProcessingStart;
        event Action FilesProcessingFinish;
    }
}
