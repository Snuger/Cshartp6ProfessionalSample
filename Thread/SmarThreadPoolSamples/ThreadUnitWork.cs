using System.Reflection.PortableExecutable;
using System.Collections.Generic;
using System;
using System.Threading;

namespace SmarThreadPoolSamples
{
    public class ThreadUnitWork : IDisposable
    {

        bool _disposabed;

        bool _isRuning;

        protected Thread thread;

        protected readonly string _wordOnQueueName;

        public ThreadUnitWork(Thread workUnitThread, string workOnQueueName)
        {
            this.WorkUnitThread = workUnitThread;
            this.WorkOnQueueName = workOnQueueName;
            this.Id = new Guid().ToString();
        }

        public Thread WorkUnitThread { get; }

        public string WorkOnQueueName { get; }


        public string Id { get; }


        public void Satart()
        {
            if (!_isRuning)
            {
                _isRuning = true;
                this.WorkUnitThread.Start();
            }

        }


        public void Stop()
        {
            if (_isRuning)
                this.WorkUnitThread.Abort();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposabed) return;
            if (disposing)
            {

            }
            _disposabed = true;
        }

    }
}