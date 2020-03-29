using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ThreadingIssues
{
    class StateObject
    {
        private int _state = 5;

        public object _sync = new object();

        public void ChangeState(int loop) {
            lock (_sync) {
                if (_state == 5)
                {
                    _state++;
                    Trace.Assert(_state == 6,
                        $"Race condition occurred atfter {loop} loops");
                }
                _state = 5;
            }         
        }
    }

    public class SampleTask
    {
        public SampleTask()
        {
        }

        public void RaceCondition(object o) {
            Trace.Assert(o is StateObject, "o must be type of StateObject");
            StateObject state = o as StateObject;
            int i = 0;
            while (true) {
                //lock (state)
                {
                    state.ChangeState(i++);
                }
               
            }
        }


    }
}
