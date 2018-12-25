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
        private bool IsMoveButton(RoutedEventArgs e) =>
           (e.Source as FrameworkElement).Name == nameof(btn_move);

        private void OnGridMoseMove(object sender, MouseEventArgs e)
        {
            if (CheckIgnoreGridMove.IsChecked == true && !IsMoveButton(e)) return;
            ShowStatus(nameof(OnGridMoseMove), e);
            e.Handled = CheckStopBubbling.IsChecked == true;
        }

        private void ShowStatus(string status, RoutedEventArgs e) {
            txt_log.Text += $"{status} source: {e.Source.GetType().Name} {(e.Source as FrameworkElement)?.Name}, original source: {e.OriginalSource.GetType().Name}";
            txt_log.Text += "\r\n";
        }

        private void OnCleanStatus(object sender, RoutedEventArgs e)
        {
            txt_log.Text = string.Empty;
        }

        private void Onbtn_move_MouseMove(object sender, MouseEventArgs e)
        {
            ShowStatus(nameof(Onbtn_move_MouseMove), e);
            e.Handled = CheckStopBubbling.IsChecked == true;
        }
    }
}
