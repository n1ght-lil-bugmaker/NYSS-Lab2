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
using LABA_2.Model;

namespace LABA_2
{
    /// <summary>
    /// Логика взаимодействия для FullDescription.xaml
    /// </summary>
    public partial class FullDescription : Window
    {
        public FullDescription()
        {
            InitializeComponent();

            OK.Click += (sender, args) =>
            {
                Close();
            };
        }

        public void Show(Threat t)
        {
            ThreatName.Content = t.Name + " - полнейшее описание";
            Description.Text = t.Description;

            Title = t.Name;

            Show();
        }
    }
}
