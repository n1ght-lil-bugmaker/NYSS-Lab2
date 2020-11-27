using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABA_2.Model
{
    class FieldNameAttribute : Attribute
    {
        public string fieldName;

        public FieldNameAttribute(string name)
        {
            fieldName = name;
        }

    }
}
