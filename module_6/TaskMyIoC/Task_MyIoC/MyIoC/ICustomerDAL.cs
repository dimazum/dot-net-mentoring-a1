namespace MyIoC
{
	public interface ICustomerDAL
	{
	}

    [Export(typeof(ICustomerDAL))]
    [ImportConstructor]
    public class CustomerDAL : ICustomerDAL
    {
        public ITest1 Test1 { get; set; }

        public CustomerDAL(ITest1 test1)
        {
            Test1 = test1;
        }
    }

    public interface ITest1
    {
    }
    [Export(typeof(ITest1))]
    public class Test1 : ITest1
    {
    }
}

