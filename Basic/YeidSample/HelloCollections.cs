using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace YeidSample
{
    public class HelloCollections
    {
        //public IEnumerator<string> GetEnumerator() {

        //    yield return "Hello Work";
        //    yield return "肯男为，伙扣";
        //}


        public IEnumerator GetEnumerator() => new Enumnerator(0);


        public class Enumnerator : IEnumerator<string>, IEnumerator, IDisposable
        {
         
            private int _state;
            private string _current;

            public Enumnerator(int state)
            {
                _state = state;
            }



            public string Current => Current;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public bool  MoveNext()
            {
                switch (_state) {
                    case 0:
                        _current = "Hello";
                        _state = 1;
                        break;
                    case 1:
                        _current = "word";
                        _state = 2;
                        break;
                }
                return false;
               // throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }

    }
}
