

namespace UnmanagedCode
{
    public class Program
    {
        static void Main(string[] args)
        {
            var batteryState = CallNtService.CallNtService.GetSystemBatteryState();
            var sleepTime = CallNtService.CallNtService.GetLastSleepTime();
            var lastWakeTime = CallNtService.CallNtService.GetLastWakeTime();
            var powerInfo = CallNtService.CallNtService.GetSystemPowerInformation();
            //CallNtService.CallNtService.SetHibernationState();
        }
    }
}
