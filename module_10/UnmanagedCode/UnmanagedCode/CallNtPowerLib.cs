using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace UnmanagedCode
{
    public static class CallNtPowerLib
    {
        [DllImport("powrprof.dll", SetLastError = true)]
        public static extern UInt32 CallNtPowerInformation(
            Int32 InformationLevel,
            IntPtr lpInputBuffer,
            UInt32 nInputBufferSize,
            IntPtr lpOutputBuffer,
            UInt32 nOutputBufferSize);
    }

    [StructLayout(LayoutKind.Sequential)]
    struct PROCESSOR_POWER_INFORMATION
    {
        public uint Number;
        public uint MaxMhz;
        public uint CurrentMhz;
        public uint MhzLimit;
        public uint MaxIdleState;
        public uint CurrentIdleState;
    }

    struct SYSTEM_BATTERY_STATE
    {
        public byte AcOnLine;
        public byte BatteryPresent;
        public byte Charging;
        public byte Discharging;
        public byte spare1;
        public byte spare2;
        public byte spare3;
        public byte spare4;
        public UInt32 MaxCapacity;
        public UInt32 RemainingCapacity;
        public Int32 Rate;
        public UInt32 EstimatedTime;
        public UInt32 DefaultAlert1;
        public UInt32 DefaultAlert2;
    }

    public struct SYSTEM_POWER_INFORMATION
    {
        public ulong MaxIdlenessAllowed;
        public ulong Idleness;
        public ulong TimeRemaining;
        public char CoolingMode;
    }
}
