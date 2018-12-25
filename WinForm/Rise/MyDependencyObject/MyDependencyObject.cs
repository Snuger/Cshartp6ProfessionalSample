using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyDependencyObjectWPF
{
   public class MyDependencyObject:UIElement
    {
        
       public int Value {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty,value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value",typeof(int),typeof(MyDependencyObject),
                new PropertyMetadata(0,OnValueChanged,CoreVerifyValue));

        private static object CoreVerifyValue(DependencyObject d, object baseValue)
        {
            int newValue=(int)baseValue;
            MyDependencyObject control = (MyDependencyObject)d;
            newValue = Math.Max(control.MinValue, Math.Min(control.MaxValue, newValue));
            return newValue;
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MyDependencyObject control = (MyDependencyObject)d;
            var e1=new RoutedPropertyChangedEventArgs<int>((int)e.OldValue,
                (int)e.NewValue,ValueChangedEvent);
            control.OnValueChanged(e1);

        }

        protected virtual void OnValueChanged(RoutedPropertyChangedEventArgs<int> args) {
            RaiseEvent(args);
        }

        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(int), typeof(MyDependencyObject), new PropertyMetadata(0));


        public static RoutedEvent ValueChangedEvent =
           EventManager.RegisterRoutedEvent(nameof(ValueChanged), RoutingStrategy.Bubble,
               typeof(RoutedPropertyChangedEventHandler<int>), typeof(MyDependencyObject));

        public event RoutedPropertyChangedEventHandler<int> ValueChanged
        {
            add
            {
                AddHandler(ValueChangedEvent, value);
            }
            remove
            {
                RemoveHandler(ValueChangedEvent, value);
            }
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
