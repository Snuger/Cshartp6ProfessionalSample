using System;
using System.Collections.Generic;
using System.Text;

namespace ThreadSample
{
    public class Singleton
    {
        private static Singleton instance;
        private static object obj = new object();
        public Singleton()
        {

        }

        public static Singleton GetInstance()
        {
            
            lock (obj)      //通过Lock关键字实现同步
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
            }               
           
            return instance;
        }
    }
}
