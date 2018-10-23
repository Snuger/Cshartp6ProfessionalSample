using System;
using System.Collections.Generic;
using System.Text;

namespace SynchronizationSample
{
   public  class Job
    {
        private ShareState _shareState;

        public Job(ShareState shareState)
        {
            this._shareState = shareState;
        }


        public void DoTheJob() {

            for (int i = 0; i <50000; i++) {

              // lock (_shareState)
                {
                    _shareState.IncrementState();
                }
            }
        }
    }
}
