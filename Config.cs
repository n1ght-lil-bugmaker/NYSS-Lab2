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
            File.WriteAllLines(Directory.GetCurrentDirectory() + @"\config.config",
                new string[]
                {
                    PathToXLSX, PathToJSON, XLSXAddress, UpdateInterval.Hours.ToString(), LastUpdate.ToString()
                });
        }

        static Config()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + @"\config.config"))
            {
                var data = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\config.config");

                PathToXLSX = data[0];
                PathToJSON = data[1];
                XLSXAddress = data[2];
                UpdateInterval = new TimeSpan(0, int.Parse(data[3]), 0, 0);
                LastUpdate = DateTime.Parse(data[4]);
            }

            else
            {
                PathToXLSX = Directory.GetCurrentDirectory() + @"\thrlist.xlsx";
                PathToJSON = Directory.GetCurrentDirectory() + @"localcash.json";
                XLSXAddress = "https://bdu.fstec.ru/files/documents/thrlist.xlsx";
                UpdateInterval = new TimeSpan(0, 5, 0, 0);
                LastUpdate = DateTime.Now;

                SaveConfiguration();
            }
            
        }
    }
}
