using CallNtService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CallNtCOM
{
    [ComVisible(true)]
    [Guid("1501FFA2-DD56-4EEC-B26F-A6FC31463751")]
    [ClassInterface(ClassInterfaceType.None)]
    public class MyCOMObj : IMyComObj
    {
        public object GetLastSleepTime()
        {
            return CallNtService.CallNtService.GetLastSleepTime();
        }

        public object GetLastWakeTime()
        {
            return CallNtService.CallNtService.GetLastWakeTime();
        }

        public object GetSystemBatteryState()
        {
            return CallNtService.CallNtService.GetSystemBatteryState();
        }

        public object GetSystemPowerInformation()
        {
            return CallNtService.CallNtService.GetSystemPowerInformation();
        }

        public void ReserveHibernationFile()
        {
            CallNtService.CallNtService.ReserveHibernationFile();
        }

        public void SetHibernationState()
        {
            CallNtService.CallNtService.SetHibernationState();
        }
    }
}
