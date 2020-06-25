using System.Runtime.InteropServices;

namespace CallNtService
{
    [ComVisible(true)]
    [Guid("BE51BB97-D442-4DB1-940E-D3111B4896B5")]
    [ClassInterface(ClassInterfaceType.None)]
    public class CallNtCOM : ICallNtCOM
    {
        public object GetLastSleepTime()
        {
            return CallNtService.GetLastSleepTime();
        }

        public object GetLastWakeTime()
        {
            return CallNtService.GetLastWakeTime();
        }

        public object GetSystemBatteryState()
        {
            return CallNtService.GetSystemBatteryState();
        }

        public object GetSystemPowerInformation()
        {
            return CallNtService.GetSystemPowerInformation();
        }

        public void ReserveHibernationFile()
        {
            CallNtService.ReserveHibernationFile();
        }

        public void SetHibernationState()
        {
            CallNtService.SetHibernationState();
        }
    }
}
