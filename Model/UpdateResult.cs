using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABA_2.Controller
{
    public class UpdateResult
    {
        public int Id { get; set; }

        public string Field { get; set; }

        public object PreviousValue { get; set; }

        public object CurrentValue { get; set; }


        
    }
}
