using System;
using System.Collections.Generic;
using System.Text;

namespace BasicObserverDesign
{
    abstract class Subject
    {
        private IList<Observer> observers = new List<Observer>();

        public void Add(Observer observer)=>observers.Add(observer);

       public void Detach(Observer observer)=>observers.Remove(observer);
       

        public void Nofify() { 
            foreach(var item in observers)            
                item.Update();            
        }
    }
}
