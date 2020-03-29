using ModelWPF;
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

namespace TemplatesWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollection<Country> Countries { get; } = new ObservableCollection<Country>();

        public MainWindow()
        {           

            InitializeComponent();
            this.DataContext = this;
            this.countryButton.Content = new Country
            {
                Name = "Austria",
                ImagePath = "/Images/Austria.bmp"
            };

            var countries = new CountryRepository().GetCountries();
            foreach (var country in countries) {
                Countries.Add(country);
            }
        }

        private void On_Default_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("你妹啊,可以啊!");
        }
    }
}
