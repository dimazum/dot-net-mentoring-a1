using System;
using System.Runtime.InteropServices;
using UnmanagedCode.enums;

namespace UnmanagedCode
{
    class Program
    {
        [DllImport("powrprof.dll", SetLastError = true)]
        public static extern UInt32 CallNtPowerInformation(
            Int32 InformationLevel,
            IntPtr lpInputBuffer,
            UInt32 nInputBufferSize,
            IntPtr lpOutputBuffer,
            UInt32 nOutputBufferSize);


        [DllImport("Powrprof.dll", SetLastError = true)]
        static extern uint SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);


        static void Main(string[] args)
        {
            //SethibernationState();
            //var info = GetProcesorInfo();
            var batteryState = GetSystemBatteryState();
            var sleepTime = GetLastSleepTime();
            var lastWakeTime = GetLastWakeTime();
            var powerInfo = GetSystemPowerInformation();
        }


        private static object GetSystemBatteryState()
        {
            var size = Marshal.SizeOf<SYSTEM_BATTERY_STATE>();
            var outputBuffer = Marshal.AllocCoTaskMem(size);
            uint retval = CallNtPowerInformation(
                (int)POWER_INFORMATION_LEVEL.SystemBatteryState,
                IntPtr.Zero,
                0,
                outputBuffer,
                (uint)Marshal.SizeOf(typeof(SYSTEM_BATTERY_STATE))
            );

            var batteryState = Marshal.PtrToStructure(outputBuffer, typeof(SYSTEM_BATTERY_STATE));
            return batteryState;
        }

        private static object GetProcesorInfo()
        {
            var size = Marshal.SizeOf<PROCESSOR_POWER_INFORMATION>();
            var outputBuffer = Marshal.AllocCoTaskMem(size);

            int procCount = Environment.ProcessorCount;
            PROCESSOR_POWER_INFORMATION[] procInfo =
                new PROCESSOR_POWER_INFORMATION[procCount];
            uint retval = CallNtPowerInformation(
                (int)POWER_INFORMATION_LEVEL.ProcessorInformation,
                IntPtr.Zero,
                0,
                outputBuffer,
                (uint)(procInfo.Length * Marshal.SizeOf(typeof(PROCESSOR_POWER_INFORMATION)))
            );

            var batteryState = Marshal.PtrToStructure(outputBuffer, typeof(PROCESSOR_POWER_INFORMATION));

            return batteryState;
        }

        private static object GetLastSleepTime()
        {
            var size = Marshal.SizeOf<ulong>();
            var outputBuffer = Marshal.AllocCoTaskMem(size);

            uint retval = CallNtPowerInformation(
                (int)POWER_INFORMATION_LEVEL.LastSleepTime,
                IntPtr.Zero,
                0,
                outputBuffer,
                (uint)(Marshal.SizeOf(typeof(ulong)))
            );

            var lastSleepTime = Marshal.ReadInt32(outputBuffer);

            return lastSleepTime;
        }

        private static object GetLastWakeTime()
        {
            var size = Marshal.SizeOf<ulong>();
            IntPtr outputBuffer = Marshal.AllocCoTaskMem(size);

            uint retval = CallNtPowerInformation(
                (int)POWER_INFORMATION_LEVEL.LastWakeTime,
                IntPtr.Zero,
                0,
                outputBuffer,
                (uint)(Marshal.SizeOf(typeof(ulong)))
            );

            var lastSleepTime = Marshal.PtrToStructure(outputBuffer, typeof(ulong));

            return lastSleepTime;
        }

        private static object GetSystemPowerInformation()
        {
            var size = Marshal.SizeOf<SYSTEM_POWER_INFORMATION>();
            var outputBuffer = Marshal.AllocCoTaskMem(size);

            uint retval = CallNtPowerInformation(
                (int)POWER_INFORMATION_LEVEL.SystemPowerInformation,
                IntPtr.Zero,
                0,
                outputBuffer,
                (uint)(Marshal.SizeOf(typeof(SYSTEM_POWER_INFORMATION)))
            );

            var powerInformation = Marshal.PtrToStructure(outputBuffer, typeof(SYSTEM_POWER_INFORMATION));

            return powerInformation;
        }

        private static void SethibernationState()
        {
            var result = SetSuspendState(true, true, true);
        }

        private void ReserveHibernationFile()
        {
            int sizeOfInputBuffer = Marshal.SizeOf<UInt32>();
            uint sizeOfOutputBuffer = 0;
            IntPtr inputBuffer = Marshal.AllocHGlobal(sizeOfInputBuffer);

            Marshal.WriteInt32(inputBuffer, 0);

            var statusResult = CallNtPowerInformation(
                (int)POWER_INFORMATION_LEVEL.SystemReserveHiberFile,
                inputBuffer,
                (uint)sizeOfInputBuffer,
                IntPtr.Zero,
                sizeOfOutputBuffer);
        }
    }
}
