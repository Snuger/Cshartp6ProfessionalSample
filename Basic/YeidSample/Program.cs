using System;
using System.Collections;

namespace YeidSample
{
    class Program
    {
        static void Main(string[] args)
        {

            HelloCollections hello = new HelloCollections();
           var _hello= hello.GetEnumerator();
            while (_hello.MoveNext()) {
                Console.WriteLine(_hello.Current.ToString());
            }

            Console.ReadLine();
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
            MusicTitles musicTitles = new MusicTitles();
            var music= musicTitles.GetEnumerator();
            while (music.MoveNext()) {
                Console.WriteLine(music.Current.ToString());
            }           
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");


            foreach (string arg in musicTitles.Reverse()) {
                Console.WriteLine(arg);
            }

            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
            Console.ReadLine();


            var games = new GameMoves();
            IEnumerator enumerator = games.Corss();
            while (enumerator.MoveNext()) {
                enumerator = enumerator.Current as IEnumerator;
            }

            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
            Console.ReadLine();

            var tuper = tuple(99, 10);
            


        }


        public static Tuple<int, int> tuple(int a, int b) {
            int result = a;
            int colparesult = a * b;
            return Tuple.Create(result, colparesult);
        }
    }
}
