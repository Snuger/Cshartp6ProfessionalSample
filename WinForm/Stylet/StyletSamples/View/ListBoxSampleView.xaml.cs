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

namespace StyletSamples.View
{
    /// <summary>
    /// ListBoxSampleView.xaml 的交互逻辑
    /// </summary>
    public partial class ListBoxSampleView : Page
    {
        public ListBoxSampleView()
        {
<<<<<<< HEAD:WinForm/Stylet/StyletSamples/View/ListBoxSampleView.xaml.cs
            InitializeComponent();
=======
            InitializeComponent();          
        }   

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listbox = sender as ListBox;          
            this.lab_SelectText.Content =$"你选中了:{ listbox.SelectedItem.ToString()}";

>>>>>>> a5c36f47287ed06223919170986c5bf95289755c:WinForm/Stylet/StyletSamples/View/ListBoxSample.xaml.cs
        }
    }
}
