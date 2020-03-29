using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.ViewModel
{
    public class CalcuatorResultViewModel:NotifyCationObject
    {

        private string compareStr;

        public string CompareStr
        {
            get { return compareStr; }
            set
            {
                compareStr = value;
                this.RaisePropertyChanged("CompareStr");
            }
        }

        private double parameter;

        public double Parameter
        {
            get { return parameter; }
            set
            {
                parameter = value;
                this.RaisePropertyChanged("Parameter");
            }
        }



    }
}
