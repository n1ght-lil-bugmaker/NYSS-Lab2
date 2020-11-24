using LABA_2.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LABA_2.Model
{
    /// <summary>
    /// Логика взаимодействия для ShowFullThreat.xaml
    /// </summary>
    public partial class ShowFullThreat : Window
    {
        public ShowFullThreat()
        {
            InitializeComponent();
        }

        public void Show(int ThreatId)
        {
            try
            {
                var threat = ThreatController.ThreatCollection
                    .Where(x => x.Id == ThreatId)
                    .FirstOrDefault()
                    .GetSource();

                IDLabel.Content = threat.Id.ToString();
                NameLabel.Content = threat.Name;
                SourceLabel.Content = GetThreatSource(threat);
                ObjectLabel.Content = threat.ImpactObject;
                ConfidentialLabel.Content = GetCorrectBooleanInterpritation(threat.BreachOfConfidentiality);
                IntegrityLabel.Content = GetCorrectBooleanInterpritation(threat.BreachOfIntegrity);
                AvailabilityLabel.Content = GetCorrectBooleanInterpritation(threat.BreachOfAvailability);
                IncludeDateLabel.Content = threat.DateOfIncluding.ToString("dd.MM.yyyy");
                LastUpdateLabel.Content = threat.DateOfLastChange.ToString("dd.MM.yyyy");

                Title = threat.Name;

                GetFullDescription.Click += (sender, args) =>
                {
                    new FullDescription().Show(threat);
                };

                Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace, ex.Source);
                
            }
            finally
            {
                //Close();
            }
        }

        private string GetThreatSource(Threat t)
        {
            var res = "";

            foreach(var source in t.Sources)
            {
                res += $"{source}. ";
            }

            return res;
        }

        private string GetCorrectBooleanInterpritation(bool param)
        {
            if(param)
            {
                return "Есть пробитие!";
            }
            else
            {
                return "Броня не пробита!";
            }
        }
    }
}
