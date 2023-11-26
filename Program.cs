using lab2.Funkcje;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IFunction Funkcja = FabrykaFunkcji.WybierzFunkcje();
            Console.Write(Funkcja.Name);
            Console.ReadKey();
        }

    }
}

