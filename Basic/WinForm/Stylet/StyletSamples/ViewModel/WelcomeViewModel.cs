using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stylet;

namespace StyletSamples.ViewModel
{
    public class WelcomeViewModel:Screen
    {

        private string name="张三";

        public string Name
        {
            get { return name; }
            set { SetAndNotify(ref name, value); }
        }


        public void SayHello() => Name = $"你好,{Name}";
    }
}
