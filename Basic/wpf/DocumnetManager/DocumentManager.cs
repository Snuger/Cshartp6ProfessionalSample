using System;
using System.Collections.Generic;
using System.Text;

namespace DocumnetManager
{
    public class DocumentManager<TDcoumnet> where TDcoumnet :IDocumnet
    {
        private readonly Queue<TDcoumnet> docuemnetQueue = new Queue<TDcoumnet>();

       

        public bool IsEmpty => docuemnetQueue.Count > 0;


        public void Add(TDcoumnet doc)
        {
            lock (this)
            {
                docuemnetQueue.Enqueue(doc);
            }
        }


        public TDcoumnet GetDocument() {
            TDcoumnet doc = default(TDcoumnet);
            lock (this) {

                doc = docuemnetQueue.Dequeue();
            }
            return doc;
        }


        public void DisplayAllDocumnet() {
            foreach(TDcoumnet doc in docuemnetQueue)
            {
                Console.WriteLine(doc.Title);

            }
        }
    }
}
