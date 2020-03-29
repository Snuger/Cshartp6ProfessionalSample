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

namespace AttachedPropertyDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MyAttachedPropertyProvider.SetMySample(button5, "btn5");
        }

        private void OnButton7_Click(object sender, RoutedEventArgs e)
        {
            var str=MyAttachedPropertyProvider.GetMySample((Button)sender);
            MessageBox.Show(str);
        }

        private void OnButton8_click(object sender, RoutedEventArgs e)
        {
            foreach (var item in LogicalTreeHelper.GetChildren(grid1).OfType<FrameworkElement>().Where(u=>MyAttachedPropertyProvider.GetMySample(u) != string.Empty)) {
                listbox1.Items.Add($"{item.Name}:{MyAttachedPropertyProvider.GetMySample(item)}");
            }

            foreach (var item in GetChildren(grid1, u => true).Where(m => MyAttachedPropertyProvider.GetMySample(m) != string.Empty)) {
                listbox1.Items.Add($"{item.Name}:{MyAttachedPropertyProvider.GetMySample(item)}");
            }

            foreach (var item in GetChildren(grid1, u => MyAttachedPropertyProvider.GetMySample(u)!=string.Empty))
            {
                listbox1.Items.Add($"{item.Name}:{MyAttachedPropertyProvider.GetMySample(item)}");
            }


            foreach (var item in GetChildren(grid1, u => { return !string.IsNullOrEmpty(MyAttachedPropertyProvider.GetMySample(u)); }))
            {
                listbox1.Items.Add($"{item.Name}:{MyAttachedPropertyProvider.GetMySample(item)}");
            }

        }

        private IEnumerable<FrameworkElement> GetChildren(FrameworkElement element, Func<FrameworkElement, bool> pred) {
            int childrenCount = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < childrenCount; i++) {
                FrameworkElement child = VisualTreeHelper.GetChild(element, i) as FrameworkElement;
                if (child != null && pred(child))
                {
                    yield return child;
                }
            }
        }
    }
}
