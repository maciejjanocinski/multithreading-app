using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Funkcje
{
    internal class Funkcja1 : IFunction
    {
        string Name = "2 * x + 2 * Math.Pow(x, 2)";
        Func<double, double> Func = x => 2 * x + 2 * Math.Pow(x, 2);

        public string name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Func<double, double> Function { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    }
}

