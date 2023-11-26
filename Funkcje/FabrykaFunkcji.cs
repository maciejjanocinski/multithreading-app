using System;
using System.Collections.Generic;

namespace lab2.Funkcje
{
    internal class FabrykaFunkcji
    {
      private static List<IFunction> GenerujFuncje()
        {
            return new List<IFunction>
            {
                new Funkcja1(),
                new Funkcja2()
            };
        }

       public static IFunction WybierzFunkcje()
        {
            List<IFunction> functions = GenerujFuncje();

            int i = 0;
            foreach (IFunction function in functions)
            {
                Console.WriteLine(i + 1 + ". " + function.GetName());
                i++;
            }

            Console.WriteLine("Wybrana funkcja: ");
            int NumerWybranejFunkcji = int.Parse(Console.ReadLine());

            return functions[NumerWybranejFunkcji - 1];
        }
    }
}
