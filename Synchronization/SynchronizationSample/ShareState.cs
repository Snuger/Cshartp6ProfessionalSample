using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SynchronizationSample
{
    public  class ShareState
    {
  
        private int _state = 0;

        private object _syncRoot = new object();

        public int State { get => _state; set => _state = value; }


        public int IncrementState() {

            lock (_syncRoot) {
                return ++_state;
            }
           
        }
    }


    public class ShareStateObject {

        private int _state;

        public int State {
            get {               
              return Interlocked.Increment(ref _state);                   
            }
            set {
                _state = value;
            }
        }
    }
}
