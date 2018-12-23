using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CodeIntroWPF
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var button = new Button() {
                Content="Click Me!"               
            };
            button.Click += (sender, e) =>
            {
                button.Content = "clicked";
            };

            var window = new Window()
            {
                Title="Window demo",
                Content=button
            };

            var app = new Application();
            app.Run(window);

            


        }
    }
}
