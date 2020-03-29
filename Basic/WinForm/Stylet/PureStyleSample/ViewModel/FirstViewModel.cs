using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureStyleSample.ViewModel
{
   public  class FirstViewModel:Screen
    {
        private int age;

        public int Age
        {
            get { return age; }
            set
            {
                SetAndNotify(ref this.age, value);
            }
        }

    }
}
