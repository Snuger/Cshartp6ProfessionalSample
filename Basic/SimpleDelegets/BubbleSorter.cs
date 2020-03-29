using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleDelegets
{
   public class BubbleSorter
    {
        public static void Sort<T>(IList<T> employee, Func<T, T, bool> comparison) {

            bool swaped = true;
            do
            {
                swaped = false;
                for (int t = 0; t < employee.Count - 1; t++) {
                    if (comparison(employee[t],employee[t+1]))
                    {
                        T temp = employee[t];
                        employee[t] = employee[t + 1];
                        employee[t + 1] = temp;
                        swaped = true;
                    }
                }
            } while (swaped);




        }
    }
}
