using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Funkcje
{
    internal interface IFunction
    {
         string name { get; set; }
         Func<double, double> Function { get; set; }
    }
}