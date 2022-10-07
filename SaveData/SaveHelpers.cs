using System;
using System.Collections.Generic;
using System.Text;

namespace WeaponSelector
{
    internal static class ArrayExtensions
    {
        public static string IndexIfItExists(this string[] array, int index)
        {
            return index < array.Length ? array[index] : string.Empty;
        }
    }
}
