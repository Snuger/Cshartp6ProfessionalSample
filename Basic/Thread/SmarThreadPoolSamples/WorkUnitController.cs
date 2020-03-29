using System.Linq;
using System.Collections.Generic;
using System;
using System.Threading;
 

namespace SmarThreadPoolSamples
{
    public class WorkUnitController
    {

       protected object workLockObject=new object();

        public  List<ThreadUnitWork>  works;

        public WorkUnitController()
        {
            this.works = new List<ThreadUnitWork>();            

        }

        public void add(ThreadUnitWork work)
        {
            lock (workLockObject)
            {
                this.works.Add(work);
            }

        }

        public void Remove(string  id){
            lock(workLockObject){
                ThreadUnitWork _work=   this.works.Where(c=>c.Id==id) as ThreadUnitWork;
                _work.Stop();
                this.works.Remove(_work);                                
            }
        }







    }
}