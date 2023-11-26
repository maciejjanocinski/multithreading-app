using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Funkcje
{
    internal class Funkcja2 : IFunction
    {
        readonly String Name = "y = 2x^2";
        readonly Func<double, double> Func = x => 2 * Math.Pow(x, 2);
        public string GetName() { return Name; }
    }
}
