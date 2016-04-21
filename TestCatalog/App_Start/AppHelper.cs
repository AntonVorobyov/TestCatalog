using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TestCatalog
{
    public static class AppHelper
    {
        private static readonly Random _random = new Random();

        public static string GeneratePhone()
        {
            return $"+{_random.Next(1, 9)}-({_random.Next(800, 999):000})-{_random.Next(0, 999):000}-{_random.Next(0, 99):00}-{_random.Next(0, 99):00}";
        }

        public static ICollection<string> GeneratePhones()
        {
            return Enumerable.Range(0, 100).Select(x => GeneratePhone()).ToList();
        }

        public static ICollection<string> GetCounties()
        {
            var cultureList = new List<string>();

            foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures))
            {
                try
                {
                    RegionInfo region = new RegionInfo(culture.LCID);

                    if (!(cultureList.Contains(region.DisplayName)))
                    {
                        cultureList.Add(region.DisplayName);
                    }
                }
                catch (ArgumentException e)
                {
                    continue;
                }
            }
            return cultureList.OrderBy(c => c).ToList();
        }
    }
}