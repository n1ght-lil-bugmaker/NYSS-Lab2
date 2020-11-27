using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LABA_2.Model
{
    public class ThreatShortForm
    {
        public int Id { get; }

        public string Name { get; }

        private Threat _source;
        public ThreatShortForm(Threat threat)
        {
            Id = threat.Id;
            Name = threat.Name;

            _source = threat;
        }

        

        public Threat GetSource()
        {
            return _source;
        }

        public bool CheckName(string request)
        {
            return Name.ToLower().Contains(request.ToLower());
        }


    }
}
