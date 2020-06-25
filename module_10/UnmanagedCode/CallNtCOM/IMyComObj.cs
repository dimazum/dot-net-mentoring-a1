using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CallNtCOM
{
    [ComVisible(true)]
    [Guid("2E87C908-A26B-4B9A-ACFC-2711800D9330")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IMyComObj
    {
        object GetSystemBatteryState();
        object GetLastSleepTime();
        object GetLastWakeTime();
        object GetSystemPowerInformation();
        void SetHibernationState();
        void ReserveHibernationFile();
    }
}
