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
        public UpdatePage(IEnumerable<UpdateResult> updates)
        {
            InitializeComponent();
            _collection = updates;
            UpdatedTable.ItemsSource = _collection;


            UpdatedCountLabel.Content = CountUpdated();
            ResultLabel.Content = "Успешно";
            OK.Click += (sender, args) =>
            {
                Close();
            };
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
    }
}
