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
using System.IO.Ports;


namespace UartSamples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           //SerialPort serialPort = new SerialPort() {
           //    BaudRate = 115200,
           //    Encoding = Encoding.UTF8,
           //    ReadTimeout = 500,
           //    WriteTimeout = 500,
           //};
           // serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
        }

        //private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}  
    }
}
