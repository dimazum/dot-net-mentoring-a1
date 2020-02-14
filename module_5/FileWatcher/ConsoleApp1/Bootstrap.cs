using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Bootstrap
    {
        public static void Start()
        {
            var fileWatcher = new FileWatcher((WatcherConfigurationSection)ConfigurationManager.GetSection("watcherSection"));

            fileWatcher.StartWatching();
        }
    }
}
