using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIoC
{
	[ImportConstructor]
	public class CustomerBLL
	{
        public ICustomerDAL DAL { get; set; }
        public Logger Logger { get; set; }

        public CustomerBLL(ICustomerDAL dal, Logger logger)
        {
            DAL = dal;
            Logger = logger;
        }
	}

	public class CustomerBLL2
	{
		[Import]
		public ICustomerDAL CustomerDAL { get; set; }
		[Import]
		public Logger logger { get; set; }
	}
}
