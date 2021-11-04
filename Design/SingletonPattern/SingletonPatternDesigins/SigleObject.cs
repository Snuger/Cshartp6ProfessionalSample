using System;

namespace SingletonPatternDesigins
{
    public class SigleObject
    {
        public static SigleObject instance=new SigleObject();       

        private SigleObject()
        {
        }

        public string SayHello(string str) => $"hellow{str}"; 
 
    }
}
