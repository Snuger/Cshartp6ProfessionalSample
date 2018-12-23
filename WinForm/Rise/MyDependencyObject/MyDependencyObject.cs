using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyDependencyObject
{
   public class MyDependencyObject:DependencyObject
    {
        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(int), typeof(MyDependencyObject), new PropertyMetadata(0));


        public static void OnMaxValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {

            int oldValue = (int)e.OldValue;
            int newValue = (int)e.NewValue;
        }


        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(int), typeof(MyDependencyObject), new PropertyMetadata(0));
    }
}
