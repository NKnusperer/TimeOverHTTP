using System;
using System.Runtime.InteropServices;

namespace TimeOverHttp
{
    public class SetTime
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetSystemTime(ref time newTime);

        struct time
        {
            public short Year;
            public short Month;
            public short DayOfWeek;
            public short Day;
            public short Hour;
            public short Minute;
            public short Second;
        }

        public void SystemTime(DateTime newTime)
        {
            newTime = newTime.ToUniversalTime();
            time setTime = new time();
            setTime.Year = (short)newTime.Year;
            setTime.Month = (short)newTime.Month;
            setTime.DayOfWeek = (short)newTime.DayOfWeek;
            setTime.Day = (short)newTime.Day;
            setTime.Hour = (short)newTime.Hour;
            setTime.Minute = (short)newTime.Minute;
            setTime.Second = (short)newTime.Second;

            SetSystemTime(ref setTime);
        }
    }
}