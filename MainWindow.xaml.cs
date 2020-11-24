using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LABA_2.Controller;
using LABA_2.Model;
using System.IO;

namespace LABA_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                if(!File.Exists(Config.PathToXLSX))
                {
                    new StartPage().ShowDialog();
                }
                else
                {
                    ThreatController.InitializeViaJSON();
                }
                
                Table.ItemsSource = ThreatController.ThreatCollection;
                ShowFullButton.Click += GetFullInfo;
                LastUpdateLabel.Content = Config.LastUpdate.ToString("dd.MM.yyyy HH:mm");
                UpdateButton.Click += UpdateClick;

                SettingsButton.Click += (sender, args) =>
                {
                    new SettingsPage().ShowDialog();
                };

                FindButton.Click += Find;
                RefreshButton.Click += (sender, args) =>
                {
                    Table.ItemsSource = ThreatController.ThreatCollection;
                    FindBox.Text = "";
                };

                FindBox.GotFocus += (sender, args) =>
                {
                    if(FindBox.Text == "Введите название...")
                    {
                        FindBox.Text = "";
                    }
                };
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace, ex.Source );
            }
            finally
            {
                //Close();
            }


        }

        public void GetFullInfo(object sender, EventArgs args)
        {
            if(Table.SelectedItem != null)
            {
                var shortForm = (Table.SelectedItem as ThreatShortForm);
                var fullInfoScreen = new ShowFullThreat();
                fullInfoScreen.Show(shortForm.Id);
                
            }
            else
            {
                MessageBox.Show("Ты видать не разобрался еще, но ничего, бывает. Просто выдели интересующую угрозу и нажми эту кнопочку)", "Твой кампутатор");
            }
        }

        public void Find(object sender, EventArgs args)
        {
            var collection = ThreatController.ThreatCollection
                .Where(x => x.CheckName(FindBox.Text));

            Table.ItemsSource = new ObservableCollection<ThreatShortForm>(collection);
        }

        public void UpdateClick(object sender, EventArgs args)
        {
            try
            {
                var updateCollection = ThreatController.Update();

                var updatePage = new UpdatePage();
                updatePage.UpdatedTable.ItemsSource = updateCollection;
                LastUpdateLabel.Content = Config.LastUpdate.ToString("dd.MM.yyyy HH:mm");

                updatePage.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace, ex.Source);
            }
        }
    }
}
