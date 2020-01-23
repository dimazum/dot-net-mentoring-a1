using FirstCharacterViewer.Initialization;
using System;
using System.Linq;
using System.Threading.Channels;
using FirstCharacterViewer.Interfaces;
using TypeConverter;

namespace FirstCharacterViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrap.Start();

            var firstCharacterController = Bootstrap.container.GetInstance<IFirstCharacterController>();
            //firstCharacterController.ConvertStringToInt();
            firstCharacterController.GetFirstCharacter();

            Console.ReadLine();
        }
 
    }
}
