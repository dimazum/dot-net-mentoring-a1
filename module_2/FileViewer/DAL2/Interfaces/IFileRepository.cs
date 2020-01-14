using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IFileRepository
    {
        IEnumerable<string> GetDirectories(string path, Func<string, bool> filter = null);
        IEnumerable<string> GetFiles(string path, Func<string, bool> filter = null);
    }
}
