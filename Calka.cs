using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab2
{
    internal class Calka
    {
         double calka_metodaProstokatow(double a, double b, Func<double, double> func, int n, int i)
        {
            double h = (b - a) / n;
            double x = a + i * h;
            return func(x);
        }

         double calka_metoda(double a, double b, Func<double, double> func, int n, int i)
        {
            double h = (b - a) / n;
            double x = a + i * h;
            return func(x);
        }
    }
}
