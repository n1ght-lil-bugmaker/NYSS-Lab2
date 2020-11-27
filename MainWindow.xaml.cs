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
using System.Timers;
using System.Windows.Threading;

namespace LABA_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer timer = new DispatcherTimer();
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
                PageBack.IsEnabled = false;

                var pager = new PageController<ThreatShortForm>(ThreatController.ThreatCollection, 15);
                pager.IsFirstPage += () => PageBack.IsEnabled = false;
                pager.IsLastPage += () => PageForward.IsEnabled = false;
                pager.NotFirstPage += () => PageBack.IsEnabled = true;
                pager.NotLastPage += () => PageForward.IsEnabled = true;


                Table.ItemsSource = pager.MoveForward();
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
                    Table.ItemsSource = pager.CurrentPage;
                    FindBox.Text = "";
                };

                FindBox.GotFocus += (sender, args) =>
                {
                    if(FindBox.Text == "Введите название...")
                    {
                        FindBox.Text = "";
                    }
                };

                var interval = Config.LastUpdate + Config.UpdateInterval - Config.LastUpdate;
                timer.Tick += AutomaticUpdate;

                if(DateTime.Now - Config.LastUpdate > interval)
                {
                    AutomaticUpdate(new object(), new EventArgs());
                }
                else
                {
                    timer.Interval = interval;
                }

                timer.Start();

                PageInfo.Content = $"1/{pager.PagesCount}";

                PageBack.Click += (sender, args) =>
                {
                    Table.ItemsSource = pager.MoveBack();
                    PageInfo.Content = pager.CurrentPageNum + $"/{pager.PagesCount}";
                };

                PageForward.Click += (sender, args) =>
                {
                    Table.ItemsSource = pager.MoveForward();
                    PageInfo.Content = pager.CurrentPageNum + $"/{pager.PagesCount}";
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

                var updatePage = new UpdatePage(updateCollection);
                LastUpdateLabel.Content = Config.LastUpdate.ToString("dd.MM.yyyy HH:mm");

                updatePage.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace, ex.Source);
            }
        }
        public void AutomaticUpdate(object sender, EventArgs args)
        {
            try
            {
                var updatedCollection = ThreatController.Update();
                var updatedPage = new UpdatePage(updatedCollection);

                updatedPage.Title = "Автообновление";
                updatedPage.UpdatedTable.ItemsSource = updatedCollection;

                updatedPage.ShowDialog();

                LastUpdateLabel.Content = Config.LastUpdate.ToString("dd.MM.yyyy HH:mm");

            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message + ex.StackTrace, ex.Source);
            }
            finally
            {
                timer.Interval = Config.UpdateInterval;
            }
            
        }
    }
}
