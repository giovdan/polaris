namespace RepoDbVsEF.Domain
{
    using System;
    using System.Linq;
    public static class DateTimeExtensions
    {
        // Convert datetime to UNIX time
        public static string ToUnixTime(this DateTime dateTime)
        {
            DateTimeOffset dto = new DateTimeOffset(dateTime.ToUniversalTime());
            return dto.ToUnixTimeSeconds().ToString();
        }

        // Convert datetime to UNIX time including miliseconds
        public static string ToUnixTimeMilliSeconds(this DateTime dateTime)
        {
            DateTimeOffset dto = new DateTimeOffset(dateTime.ToUniversalTime());
            return dto.ToUnixTimeMilliseconds().ToString();
        }

        public static int ToEpoch(this DateTime date)
        {
            TimeSpan t = date - new DateTime(1970, 1, 1);
            return (int)t.TotalSeconds;
        }
    }
}
