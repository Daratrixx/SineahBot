﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SineahBot.Extensions
{
    public static class Extensions
    {
        private static Random random = new Random();

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> collection)
        {
            return collection.OrderBy(x => random.Next(0, 10000));
        }

        public static T GetRandom<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable.Count() == 0)
                return default(T);
            return enumerable.OrderBy(x => random.Next(int.MinValue, int.MaxValue)).First();
        }

        public static char Capitalize(this char c)
        {
            return char.ToUpper(c);
        }

        public static string Capitalize(this string str)
        {
            if (str == null || str.Length == 0)
                return "";
            if (str.Length > 1)
                return str[0].Capitalize() + str.Substring(1);
            return str[0].Capitalize().ToString();
        }

        public static bool Is(this string str, params string[] values)
        {
            return values.Contains(str);
        }

        public static bool Is(this int i, params int[] values)
        {
            return values.Contains(i);
        }
    }
}
