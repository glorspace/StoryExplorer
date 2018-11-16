using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoryExplorer.Repository.Extensions
{
    public static class StringExtensions
    {
        public static TEnum ParseEnum<TEnum>(this string value) => (TEnum) Enum.Parse(typeof(TEnum), value);
    }
}
