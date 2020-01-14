using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using System.Configuration;

namespace DAL
{
    public class FileRepository : IFileRepository
    {

        
        public IEnumerable<string> GetFiles(string path, Func<string, bool> filter)
        {

            string configvalue1 = ConfigurationManager.AppSettings["countoffiles"];
         

            var files = new List<string>();

            var rootFiles = Directory.GetFiles(path);
            foreach (var file in rootFiles)
            {
                var fileInfo = new FileInfo(file);
                if (filter == null)
                {
                    files.Add(file);
                }
                else if (filter.Invoke(fileInfo.Name))
                {
                    files.Add(file);
                }
            }

            foreach (var dir in Directory.GetDirectories(path)) 
            {
                files.AddRange(GetFiles(dir, filter)); 
            }
            return files;
        }

        public IEnumerable<string> GetDirectories(string path, Func<string, bool> filter)
        {
            var files = new List<string>();

            var rootDirectories = Directory.GetDirectories(path);
            foreach (var dir in rootDirectories)
            {
                var fileInfo = new DirectoryInfo(dir);
                if (filter.Invoke(fileInfo.Name))
                {
                    files.Add(dir);
                }
                files.AddRange(GetDirectories(dir, filter));
            }

            return files;
        }

    }
}
