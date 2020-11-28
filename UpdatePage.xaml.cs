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

namespace LABA_2
{
    /// <summary>
    /// Логика взаимодействия для UpdatePage.xaml
    /// </summary>
    public partial class UpdatePage : Window
    {

        IEnumerable<UpdateResult> _collection;
        public UpdatePage(IEnumerable<UpdateResult> updates, string updateStatus)
        {
            InitializeComponent();
            _collection = updates;
            UpdatedTable.ItemsSource = _collection;
            Full.Click += GetUpdatePage;

            UpdatedCountLabel.Content = CountUpdated();
            ResultLabel.Content = updateStatus;
            OK.Click += (sender, args) =>
            {
                Close();
            };
        }

        public UpdatePage(string updateStaus)
        {
            InitializeComponent();
            ResultLabel.Content = updateStaus;
        }

        private int CountUpdated()
        {
            var IDs = new List<int>();

            foreach(UpdateResult updated in _collection)
            {
                if(!IDs.Contains(updated.Id))
                {
                    IDs.Add(updated.Id);
                }
            }

            return IDs.Count;
        }

        private void GetUpdatePage(object seder, EventArgs args)
        {
            if(UpdatedTable.SelectedItem != null)
            {
                new FullUpdateInfo(UpdatedTable.SelectedItem as UpdateResult).Show();
            }
            else
            {
                MessageBox.Show("А выделять кто будет?");
            }
        }
    }
}
