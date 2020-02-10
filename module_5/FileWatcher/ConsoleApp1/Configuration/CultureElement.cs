using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Configuration
{
    public class CultureElement : ConfigurationElement
    {

        [ConfigurationProperty("culture")]
        public string Culture
        {
            get { return (string)this["culture"]; }
        }
    }
}
