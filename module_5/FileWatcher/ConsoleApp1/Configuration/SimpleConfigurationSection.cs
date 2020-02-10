using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Configuration;

namespace ConsoleApp1
{
    public class SimpleConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("appName")]
        public string AppName
        {
            get { return (string)base["appName"]; }
        }

        [ConfigurationProperty("culture")]
        public CultureElement CultureInfo
        {
            get { return (CultureElement)this["culture"]; }
        }

        [ConfigurationProperty("folders")]
        public FolderElementCollection Folders
        {
            get { return (FolderElementCollection)this["folders"]; }
        }

        [ConfigurationProperty("rules")]
        public RuleElementCollection Rules
        {
            get { return (RuleElementCollection)this["rules"]; }
        }

        [ConfigurationProperty("defaultFolder")]
        public DefaultFolderElement DefaultFolder
        {
            get { return (DefaultFolderElement)this["defaultFolder"]; }
        }
    }
}
