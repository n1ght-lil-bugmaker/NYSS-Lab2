using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LABA_2
{
    public static class Config
    {
        public static string PathToXLSX { get; set; }
        public static string PathToJSON { get; set; }
        public static string XLSXAddress { get; set; }
        public static TimeSpan UpdateInterval { get; set; }
        public static DateTime LastUpdate { get; set; }


        public static void SaveConfiguration()
        {
            File.WriteAllLines(@"C:\Users\User\source\repos\LABA-2\config.config",
                new string[]
                {
                    PathToXLSX, PathToJSON, XLSXAddress, UpdateInterval.Hours.ToString(), LastUpdate.ToString()
                });
        }

        static Config()
        {
            var data = File.ReadAllLines(@"C:\Users\User\source\repos\LABA-2\config.config");

            PathToXLSX = data[0];
            PathToJSON = data[1];
            XLSXAddress = data[2];
            UpdateInterval = new TimeSpan(0, int.Parse(data[3]), 0, 0);
            LastUpdate = DateTime.Parse(data[4]);
        }
    }
}
