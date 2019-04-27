using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stylet;

namespace ControlSamples.ViewModel
{
    public class MenuViewModel:Screen
    {
        private string  _name;

        public string  Name
        {
            get { return _name; }
            set { SetAndNotify(ref this._name, value); }
        }


        public void SayHello() =>Name=$"Hello,{this.Name}";
    }

}
