using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LABA_2.Controller;
using System.Reflection;

namespace LABA_2.Model
{
    public class Threat
    {
        [FieldName("ID")]
        public int Id { get; set; }

        [FieldName("Название")]
        public string Name { get; set; }

        [FieldName("Описание")]
        public string Description { get; set; }

        [FieldName("источник")]
        public ThreatSource[] Sources { get; set; }

        [FieldName("Объект воздействия")]
        public string ImpactObject { get; set; }

        [FieldName("Нарушение конфиденциальости")]
        public bool BreachOfConfidentiality { get; set; }

        [FieldName("Нарушение целостности")]
        public bool BreachOfIntegrity { get; set; }

        [FieldName("Нарушение доступности")]
        public bool BreachOfAvailability { get; set; }

        [FieldName("Дата включение в БДУ")]
        public DateTime DateOfIncluding { get; set; }

        [FieldName("Дата последнего изменения в БДУ")]
        public DateTime DateOfLastChange { get; set; }

        public List<UpdateResult> CompareThreats(Threat other)
        {
            var res = new List<UpdateResult>();

            foreach (var property in typeof(Threat).GetProperties())
            {
                if (property.Name == "Id")
                {
                    continue;
                }
                else if(property.Name == "Sources")
                {
                    var u = CompareSources(other);
                    if(u != null)
                    {
                        res.Add(u);
                    }
                }
                else
                {
                    var res1 = property.GetValue(this);
                    var res2 = property.GetValue(other);
                    var attr = property.GetCustomAttribute(typeof(FieldNameAttribute));

                    if (res1.ToString() != res2.ToString())
                    {
                        res.Add(new UpdateResult()
                        {
                            Id = this.Id,
                            Field = (attr as FieldNameAttribute).fieldName,
                            PreviousValue = res1,
                            CurrentValue = res2
                        }) ;
                    }
                }
   
            }

            return res;

            

        }

        private UpdateResult CompareSources(Threat other)
        {
            var mySources = "";
            var otherSources = "";

            foreach (var sourse in Sources)
            {
                mySources += sourse.ToString() + " ";
            }

            foreach (var source in other.Sources)
            {
                otherSources += source.ToString() + " ";
            }

            if (mySources != otherSources)
            {
                return new UpdateResult()
                {
                    Id = Id,
                    Field = "Источники",
                    PreviousValue = mySources,
                    CurrentValue = otherSources
                };
            }
            else
            {
                return null;
            }
        }
    }
}








