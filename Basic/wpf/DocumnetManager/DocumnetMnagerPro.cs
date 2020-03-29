using System;
using System.Collections.Generic;
using System.Text;

namespace DocumnetManager
{
    public  class DocumnetMnagerPro<T> where T:IDocumnet
    {
        private Queue<T> queue = new Queue<T>();

        public void Add(T doc) {
            lock (this) {
                queue.Enqueue(doc);             
            }
        }

        public bool isDocumnetAble() => queue.Count > 0;

        public bool isContains(T doc) => queue.Contains(doc);

        public T GetDocumnet() {
            T documnet = default(T);

            lock (this) {
                documnet = queue.Dequeue();
            }
            return documnet;
        }

        public void DisplayAllDocumnet() {

            foreach (var doc in queue) {

               Console.WriteLine(((IDocumnet)doc).Title);               
            }
        }
    }
}
