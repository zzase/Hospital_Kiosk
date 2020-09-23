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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HKiosk.Pages.SelectHistory
{
    /// <summary>
    /// SelectHistoryPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SelectHistoryPage : Page
    {

        private void ComboBoxLoad(object sender, RoutedEventArgs e)
        {
            List<string> num = new List<string>();
            for (int i = 1; i < 10; i++)
            {
                num.Add(i.ToString());
            }

            var comboBox = sender as ComboBox;

            comboBox.ItemsSource = num;
            comboBox.SelectedIndex = 0;
        }
        private void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
            var comboBox = sender as ComboBox;

            string value = comboBox.SelectedItem as string;
            this.Title = value;
        }
        public SelectHistoryPage()
        {
            
            InitializeComponent();
        }

    }
}
