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
using LABA_2.Controller;
using LABA_2.Model;

namespace LABA_2
{
    /// <summary>
    /// Логика взаимодействия для FullUpdateInfo.xaml
    /// </summary>
    public partial class FullUpdateInfo : Window
    {
        public FullUpdateInfo(UpdateResult update)
        {
            InitializeComponent();

            FieldName.Content = update.Field;
            Previous.Text = update.PreviousValue.ToString();
            Current.Text = update.CurrentValue.ToString();

            OK.Click += (sender, args) => Close();
        }
    }
}
