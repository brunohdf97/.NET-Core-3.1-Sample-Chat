using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.DOMAIN.Extensions
{
    public static class UsefullExtensions
    {
        public static DateTime? ToDateTimeOrNull(this string str)
        {
            DateTime dtv;
            DateTime? dt = null;

            if (DateTime.TryParse(str, out dtv))
                return dtv;
            return dt;
        }

        public static DateTime? ToDateTimeOrNull(this object str)
        {
            DateTime dtv;
            DateTime? dt = null;

            if (DateTime.TryParse(str + "", out dtv))
                return dtv;
            return dt;
        }

        public static bool ToBool(this string str)
        {
            bool b = false;
            bool.TryParse(str, out b);
            return b;
        }

        public static bool ToBool(this object ob)
        {
            bool b = false;
            bool.TryParse(ob + "", out b);
            return b;
        }

        public static bool? ToBollOrNull(this object ob)
        {
            bool b;
            if (bool.TryParse(ob + "", out b))
                return b;

            return null;

        }

        public static int ToInt(this string str)
        {
            int i = 0;
            int.TryParse(str, out i);
            return i;
        }

        public static int ToInt(this object obj)
        {
            int i = 0;
            int.TryParse(obj + "", out i);
            return i;
        }

        public static int ToInt(this decimal d)
        {
            int i = 0;
            int.TryParse(d + "", out i);
            return i;
        }

        public static int ToInt(this double d)
        {
            int i = 0;
            int.TryParse(d + "", out i);
            return i;
        }

        public static int? ToIntOrNull(this string s)
        {
            int i;
            if (int.TryParse(s, out i))
                return i;

            return null;

        }

        public static decimal ToDecimal(this string s)
        {
            decimal i;
            if (decimal.TryParse(s, out i))
                return i;

            return 0;
        }

        public static decimal? ToDecimalOrNull(this string s)
        {
            decimal i;
            if (decimal.TryParse(s, out i))
                return i;

            return null;

        }

        public static double ToDouble(this string s)
        {
            double i;
            if (double.TryParse(s, out i))
                return i;

            return 0;
        }

        public static double? ToDoubleOrNull(this string s)
        {
            double i;
            if (double.TryParse(s, out i))
                return i;

            return null;

        }

    }
}
