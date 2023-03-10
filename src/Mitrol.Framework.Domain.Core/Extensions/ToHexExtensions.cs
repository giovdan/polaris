

namespace Mitrol.Framework.Domain.Core.Extensions
{
    using System.Text;

    public static class ToHexExtensions
    {
        public static string ToHex(this long number)
        {
            return ToHex(number.ToString());
        }

        public static string ToHex(this double number)
        {
            return ToHex(number.ToString());
        }

        public static string ToHex(this decimal number)
        {
            return ToHex(number.ToString());
        }

        public static string ToHex(this string str)
        {
            var sb = new StringBuilder();

            var bytes = Encoding.Unicode.GetBytes(str);
            foreach (var t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
