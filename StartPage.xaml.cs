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
using System.IO;

namespace LABA_2
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage : Window
    {
        public StartPage()
        {
            InitializeComponent();
            InfoLabel.Content = "Похоже, у вас не скачан файл (или косяк с путем). Клацай на кпопку и все будет норм)";

            DownloadButton.Click += (sender, args) =>
            {
                Controller.ThreatController.DownloadXLSX();
                Close();
            };
            Settings.Click += (sender, args) =>
            {
                new SettingsPage().ShowDialog();
                
            };
        }
    }
}
