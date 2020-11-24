using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABA_2
{
    public static class Config
    {
        public static string PathToXLSX { get; set; } = @"C:\Users\User\source\repos\LABA-2\thrlist.xlsx";
        public static string PathToJSON { get; set; } = @"C:\Users\User\source\repos\LABA-2\localcash.json";
        public static string XLSXAddress { get; set; } = @"https://bdu.fstec.ru/files/documents/thrlist.xlsx";
        public static TimeSpan UpdateInterval { get; set; } = new TimeSpan(0, 5, 0, 0);
    }
}
