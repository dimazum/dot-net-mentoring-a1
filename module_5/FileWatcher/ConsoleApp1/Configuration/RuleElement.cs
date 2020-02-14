using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Configuration
{
    public class RuleElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
        }

        [ConfigurationProperty("folder")]
        public string Folder
        {
            get
            {
                return (string)this["folder"];
            }
        }

        [ConfigurationProperty("isAddDate", IsRequired = false)]
        public bool IsAddDate
        {
            get
            {
                return (bool)this["isAddDate"];
            }
        }

        [ConfigurationProperty("isAddNumber",IsRequired = false)]
        public bool IsAddNumber
        {
            get
            {
                return (bool)this["isAddNumber"];
            }
        }
    }
}
