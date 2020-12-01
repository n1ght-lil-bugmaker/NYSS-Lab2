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
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Window
    {
        public SettingsPage()
        {
            InitializeComponent();

            try
            {
                ExcelPath.Text = Config.PathToXLSX;
                JSONPAth.Text = Config.PathToJSON;
                FilePath.Text = Config.XLSXAddress;
                Interval.Text = Config.UpdateInterval.Hours.ToString();

                Confirm.Click += (sender, args) =>
                {
                    Config.PathToXLSX = ExcelPath.Text;
                    Config.PathToJSON = JSONPAth.Text;
                    Config.XLSXAddress = FilePath.Text;
                    int t;
                    if (!int.TryParse(Interval.Text, out t) || t <= 0)
                    {
                        MessageBox.Show("Некорректные данные!", "Пепе", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        Config.UpdateInterval = new TimeSpan(0, t, 0, 0);
                        Config.SaveConfiguration();
                        Close();
                    }                   
                };
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace, ex.Source);
            }
        }
    }
}
