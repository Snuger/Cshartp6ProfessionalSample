using System;
using System.Windows;
using System.Windows.Markup;

namespace MakeupWPF
{

    public enum OperationType {

        Add,
        Subtract,
        Multiply,
        Divide
    }

    [MarkupExtensionReturnType(typeof(string))]
    public class CalculatorExtension:MarkupExtension
    {

        public double X { get; set; }

        public double Y { get; set; }

        public OperationType OperationType { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            IProvideValueTarget provideValue = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            if (provideValue != null) {
                var host = provideValue.TargetObject as FrameworkElement;
                var prop = provideValue.TargetProperty as DependencyProperty;
            }
            double result = 0;
            switch (OperationType) {
                case OperationType.Add:
                    result= X + Y;
                    break;
                case OperationType.Subtract:
                    result= X - Y;
                    break;
                case OperationType.Multiply:
                    result= X * Y;
                    break;
                case OperationType.Divide:
                    result = Y == 0 ? 0 : X / Y;
                    break;
                default:
                    throw new ArgumentException("invalid operation ");
            }
            return result.ToString();           
        }
    }
}