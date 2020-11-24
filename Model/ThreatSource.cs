using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABA_2.Model
{
    public class ThreatSource
    {
        public Violator violator;
        public Potential potential;

        public override string ToString()
        { 
            string violatorToString = "";
            string potentialToString = "";

            if(violator == Violator.External)
            {
                violatorToString = "Внешний нарушитель";
            }
            else
            {
                violatorToString = "Внутренний нарушитель";
            }

            if(potential == Potential.High)
            {
                potentialToString = "с высоким";
            }
            else if (potential == Potential.Middle)
            {
                potentialToString = "со средним";
            }
            else
            {
                potentialToString = "с низким";
            }

            return $"{violatorToString} {potentialToString} потенциалом";
            
        }

        public static ThreatSource[] GetThreatSources(string request)
        {
            var res = new List<ThreatSource>();

            foreach(var sourse in request.Split(','))
            {
                Violator toAddVialator;
                Potential toAddPotential;

                if(sourse.Contains("Внутренний"))
                {
                    toAddVialator = Violator.Internal;
                }
                else
                {
                    toAddVialator = Violator.External;
                }

                if(sourse.Contains("высоким"))
                {
                    toAddPotential = Potential.High;
                }
                else if (sourse.Contains("средним"))
                {
                    toAddPotential = Potential.Middle;
                }
                else
                {
                    toAddPotential = Potential.Low;
                }

                res.Add(new ThreatSource()
                {
                    violator = toAddVialator,
                    potential = toAddPotential
                });
                
            }

            return res.ToArray();
        }

        public static bool operator ==(ThreatSource first, ThreatSource second)
        {
            return first.potential == second.potential && first.violator == second.violator;
        }

        public static bool operator !=(ThreatSource first, ThreatSource second)
        {
            return first.potential != second.potential || first.violator != second.violator;
        }

        public override bool Equals(object obj)
        {
            return this == (obj as ThreatSource);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
