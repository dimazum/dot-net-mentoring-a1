using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CallNtService
{
    [ComVisible(true)]
    [Guid("304A9F69-1FA1-4C4C-AF76-6A5C39E36DC0")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface ICallNtCOM
    {
        object GetSystemBatteryState();
        object GetLastSleepTime();
        object GetLastWakeTime();
        object GetSystemPowerInformation();
        void SetHibernationState();
        void ReserveHibernationFile();
    }
}
