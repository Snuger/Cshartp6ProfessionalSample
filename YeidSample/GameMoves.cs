using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace YeidSample
{
   public class GameMoves
    {
        private IEnumerator _cross;
        private IEnumerator _circle;

        private int _move = 0;
        const int MaxMoves = 9;

     
        public GameMoves()
        {
            _cross = Corss();
            _circle = Circle();
        }

        public IEnumerator Corss() {
           
            while (true) {
                Console.WriteLine($"Cross,move {_move}");

                if (++_move >= MaxMoves)                
                    yield break;                
                yield return _circle;
            }         
        }

        public IEnumerator Circle() {           
            while (true) {
                Console.WriteLine($"Circle,move {_move}");
                if (++_move >= MaxMoves)               
                    yield break;               
                yield return _cross;
            }
        }
    }
}
