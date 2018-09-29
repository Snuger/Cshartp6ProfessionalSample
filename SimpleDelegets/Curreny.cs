using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleDelegets
{
    public struct Curreny
    {
        public uint Dollars;
        public ushort Ctens;

        public Curreny(uint dollars, ushort ctens)
        {
            Dollars = dollars;
            Ctens = ctens;
        }

        public override string ToString() => $"{Dollars}.${Ctens}";

        public static string GetCurrentUnity() => "Dollar";

        public static string GetDescription() => "巴唑";



        public static explicit operator Curreny(float value) {
            checked {
                uint dollars = (uint)value;
                ushort cents = (ushort)((value - dollars) * 100);
                return new Curreny(dollars, cents);
            }
        }



    }
}
