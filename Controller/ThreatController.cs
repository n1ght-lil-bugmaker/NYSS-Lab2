using LABA_2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using System.IO;
using Newtonsoft.Json;
using System.Timers;

namespace LABA_2.Controller
{
    public static class ThreatController
    {
        
        public static ObservableCollection<ThreatShortForm> ThreatCollection { get; private set; } = new ObservableCollection<ThreatShortForm>();


        public static void DownloadXLSX()
        {
            using (var wc = new WebClient())
            {
                wc.DownloadFile(Config.XLSXAddress, Config.PathToXLSX);
            }

            ThreatCollection.Clear();

            using (var fs = new FileStream(Config.PathToXLSX, FileMode.Open))
            {
                using (var reader = ExcelReaderFactory.CreateReader(fs))
                {
                    int infofields = 0;

                    while (reader.Read())
                    {
                        if (infofields < 2)
                        {
                            infofields++;
                            continue;
                        }

                        var threat = new Threat();

                        threat.Id = int.Parse(reader.GetValue(0).ToString());
                        threat.Name = reader.GetString(1);
                        threat.Description = reader.GetString(2);
                        threat.Sources = ThreatSource.GetThreatSources(reader.GetString(3));
                        threat.ImpactObject = reader.GetString(4);
                        threat.BreachOfConfidentiality = GetBoolean(reader.GetValue(5).ToString());
                        threat.BreachOfIntegrity = GetBoolean(reader.GetValue(6).ToString());
                        threat.BreachOfAvailability = GetBoolean(reader.GetValue(7).ToString());
                        threat.DateOfIncluding = reader.GetDateTime(8);
                        threat.DateOfLastChange = reader.GetDateTime(9);

                        ThreatCollection.Add(new ThreatShortForm(threat));

                        SaveToJSON();

                        Config.LastUpdate = DateTime.Now;

                    }
                }
            }

        }
        private static bool GetBoolean(string request)
        {
            return request == "1";
        }
        public static void SaveToJSON()
        {
            var serialized = JsonConvert.SerializeObject(ThreatCollection.Select(x => x.GetSource()));
            File.WriteAllText(Config.PathToJSON, serialized);
        }

        public static void InitializeViaJSON()
        {
            var collection = JsonConvert.DeserializeObject<Threat[]>(File.ReadAllText(Config.PathToJSON));

            ThreatCollection = new ObservableCollection<ThreatShortForm>(collection.Select(x => new ThreatShortForm(x)));

        }
        public static ObservableCollection<UpdateResult> Update()
        {
            var res = new ObservableCollection<UpdateResult>();
            
            var oldCollection = JsonConvert.DeserializeObject<Threat[]>(File.ReadAllText(Config.PathToJSON));
            DownloadXLSX();

            foreach (var oldThreat in oldCollection)
            {
                var toCompare = ThreatCollection.Where(x => x.Id == oldThreat.Id).FirstOrDefault();


                foreach (var compareRes in oldThreat.CompareThreats(toCompare.GetSource()))
                {
                    res.Add(compareRes);
                }
            }

            Config.LastUpdate = DateTime.Now;
            Config.SaveConfiguration();
           
            return res;
        }
    }
}
