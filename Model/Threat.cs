using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LABA_2.Controller;

namespace LABA_2.Model
{
    public class Threat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ThreatSource[] Sources { get; set; }
        public string ImpactObject { get; set; }
        public bool BreachOfConfidentiality { get; set; }
        public bool BreachOfIntegrity { get; set; }
        public bool BreachOfAvailability { get; set; }
        public DateTime DateOfIncluding { get; set; }
        public DateTime DateOfLastChange { get; set; }

        public List<UpdateResult> CompareThreats(Threat other)
        {
            var res = new List<UpdateResult>();

            foreach(var property in typeof(Threat).GetProperties())
            {
                if(property.Name == "Id")
                {
                    continue;
                }

                var res1 = property.GetValue(this);
                var res2 = property.GetValue(other);

                if(res1.ToString() != res2.ToString())
                {
                    res.Add(new UpdateResult()
                    {
                        Id = this.Id,
                        PreviousValue = res1,
                        CurrentValue = res2
                    });
                }
            }

            return res;

            /*if(Name != other.Name)
            {
                res.Add(new UpdateResult()
                {
                    Id = this.Id,
                    Field = "Name",
                    PreviousValue = Name,
                    CurrentValue = other.Name
                });
            }

            if (Description != other.Description)
            {
                res.Add(new UpdateResult()
                {
                    Id = this.Id,
                    Field = "Description",
                    PreviousValue = Description,
                    CurrentValue = other.Description
                });
            }

            if (this.GetThreatSource() != other.GetThreatSource())
            {
                res.Add(new UpdateResult()
                {
                    Id = this.Id,
                    Field = "ThreatSource",
                    PreviousValue = Sources,
                    CurrentValue = other.Sources
                });
            }

            if (ImpactObject != other.ImpactObject)
            {
                res.Add(new UpdateResult()
                {
                    Id = this.Id,
                    Field = "ImpactObject",
                    PreviousValue = ImpactObject,
                    CurrentValue = other.ImpactObject
                });
            }*/

        }

        private string GetThreatSource()
        {
            var res = "";

            foreach (var source in Sources)
            {
                res += $"{source}. ";
            }

            return res;
        }
    }
}
