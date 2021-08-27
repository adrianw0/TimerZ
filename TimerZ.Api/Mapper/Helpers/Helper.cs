
using System;

namespace TimerZ.Api.Mapper.Helpers
{
    public static class Helper
    {
        public static int CalculateElapsedTime(DateTime startTime, DateTime endTime)
        {
            TimeSpan span = endTime - startTime;
            return (int) span.TotalMilliseconds;
        }
    }
}
