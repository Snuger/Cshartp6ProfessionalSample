using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experiment
{
    public static class ExperimentOne
    {

        static ExperimentOne() => Console.WriteLine("小心点儿,");


        public static void Say() => Console.WriteLine("大爷,你的内库要掉了");

        public static void Say1() => Console.WriteLine("小伙子，你来了");
    }
}
