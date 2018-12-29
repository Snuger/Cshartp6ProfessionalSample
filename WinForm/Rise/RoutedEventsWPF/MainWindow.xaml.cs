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

namespace RoutedEventsWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnCleanStatus(object sender, RoutedEventArgs e)
        {
            txt_log.Text = string.Empty;
        }


        private bool IsMoveButton(RoutedEventArgs e) =>
           (e.Source as FrameworkElement).Name == nameof(btn_move);

        private void ShowStatus(string status, RoutedEventArgs e) {
            txt_log.Text += $"{status} source: {e.Source.GetType().Name} {(e.Source as FrameworkElement)?.Name}, original source: {e.OriginalSource.GetType().Name}";
            txt_log.Text += "\r\n";
        } 
    

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {   
            ShowStatus(nameof(OnMouseLeftButtonDown), e);
            e.Handled = CheckStopBubbling.IsChecked == true;
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowStatus(nameof(OnPreviewMouseLeftButtonDown), e);
            e.Handled =CheckStopPreview.IsChecked == true;
        }

        private void OnGridPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {           
            ShowStatus(nameof(OnGridPreviewMouseLeftButtonDown), e);
            e.Handled = CheckStopPreview.IsChecked == true;
        }

        private void OnGridMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {           
            ShowStatus(nameof(OnGridMouseLeftButtonDown), e);
            e.Handled = CheckStopBubbling.IsChecked == true;

        }
    }
}
