using System;
using System.Collections.Generic;
using System.Text;

namespace BasicObserverDesign
{
    class ConnectObserver : Observer
    {
        private string name;
        private string observerState;

 

        public ConnectSubject Subject
        {
            get;set;
        }

        public ConnectObserver(string name,ConnectSubject connectSubject)
        {
            Subject = connectSubject;
            this.name = name;
        }

        public override void Update()
        {
            observerState = Subject.SubjectState;
            Console.WriteLine($"观察者{name}的新状态{observerState}");
        }
    }
}
