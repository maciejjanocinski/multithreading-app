using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace lab2
{
    internal class BackgroundWorkerExample
    {
        Calka calka = new Calka();
        static Func<double, double> func;
        static String funcToString;
        private static BackgroundWorker[] backgroundWorkers;
        private static List<CustomArguments> args;
        static int n;

        public void Run()
        {
            PrintMenu();
            args = GenerateArgs(func);
            backgroundWorkers = new BackgroundWorker[args.Count];

            for (int i = 0; i < args.Count; i++)
            {
                backgroundWorkers[i] = new BackgroundWorker();
                backgroundWorkers[i].DoWork += BackgroundWorker_DoWork;
                backgroundWorkers[i].RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
                backgroundWorkers[i].WorkerSupportsCancellation = true;
                backgroundWorkers[i].WorkerReportsProgress = true;
                backgroundWorkers[i].ProgressChanged += BackgroundWorker_ProgressChanged;
                backgroundWorkers[i].RunWorkerAsync(args[i]);
            }

            Console.WriteLine("Naciśnij 'c', aby przerwać\n");

            while (Array.Exists(backgroundWorkers, worker => worker.IsBusy))
            {
                Cancel();
                Thread.Sleep(100);
            }

            Console.Write("Naciśnij dowolny przycisk, aby wyjść...");
            Console.ReadKey();
        }


        private static void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument is CustomArguments arguments)
            {
                var worker = sender as BackgroundWorker;
                double a = arguments.a;
                double b = arguments.b;
                Boolean czyMetodaProstokatow = arguments.czyMetodaProstokatow;
                Func<double, double> func = arguments.func;

                if (czyMetodaProstokatow)
                {
                    obliczanieMetodaProstokatow(a, b, worker, e);
                }
                else
                {
                    obliczanieMetodaTrapezow(a, b, func, worker, e);
                }
            }
        }

        private static void obliczanieMetodaTrapezow(double a, double b, Func<double, double> function, BackgroundWorker worker, DoWorkEventArgs e)
        {
            if (e.Argument is CustomArguments arguments)
            {
                var func = function;
                double poczatek = arguments.a;
                double koniec = arguments.b;

                double h = (b - a) / n;
                double result = 0;

                for (int i = 1; i <= n; i++)
                {
                    if (!worker.CancellationPending)
                    {

                        if (i % (n / 10) == 0)
                        {
                            worker.ReportProgress(i * 10 / n, " Metoda trapezów - " + "przedział (" + poczatek + ", " + koniec + "): " + result);
                            Thread.Sleep(500);
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                e.Result = result;
            }
        }

        private static void obliczanieMetodaProstokatow(double a, double b, BackgroundWorker worker, DoWorkEventArgs e)
        {
            if (e.Argument is CustomArguments arguments)
            {
                var func = arguments.func;
                double poczatek = arguments.a;
                double koniec = arguments.b;

                double h = (b - a) / n;
                double sum = 0;

                for (int i = 1; i <= n; i++)
                {
                    if (!worker.CancellationPending)
                    {
                        double x = a + i * h;
                        sum += func(x);

                        if (i % (n / 10) == 0)
                        {
                            worker.ReportProgress(i * 10 / n, " Metoda prostokątów - " + "przedział (" + poczatek + ", " + koniec + "): " + sum);
                            Thread.Sleep(500);
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                    }

                }

                e.Result = sum;
            }
        }

        private static void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Console.WriteLine("Operacja w tle została anulowana.");

            }
            else
            {
                double result = (double)e.Result;
                Console.WriteLine("Wynik całkowania: " + funcToString + " wynosi: " + +result);
            }
        }

        private static void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine($"Postęp {e.ProgressPercentage * 10}%: {e.UserState}");
        }

        public static void PrintMenu()
        {
            Console.WriteLine("Wybierz funkcję: ");
            Console.WriteLine("1. y = 2x + 2x^2");
            Console.WriteLine("2. y = 2x^2");
            Console.WriteLine("3. y = 2x - 3");
            Console.WriteLine("4. y = 2x^3 + 3x^2 - 5x + 4");

            int choice = int.Parse(Console.ReadLine());
            Console.Clear();

            while (n <= 9)
            {
                Console.WriteLine("Podaj krok 'n' (n >= 10) : ");
                n = int.Parse(Console.ReadLine());
            }


            switch (choice)
            {
                case 1:
                    func = x => 2 * x + 2 * Math.Pow(x, 2);
                    funcToString = "y = 2x + 2x^2";
                    break;
                case 2:
                    func = x => 2 * Math.Pow(x, 2);
                    funcToString = "y = 2x^2";
                    break;
                case 3:
                    func = x => 2 * x - 3;
                    funcToString = "y = 2x - 3";
                    break;
                case 4:
                    func = x => 2 * Math.Pow(x, 3) + 3 * Math.Pow(x, 2) - 5 * x + 4;
                    funcToString = "y = 2x^3 + 3x^2 - 5x + 4";
                    break;
                default:
                    Console.WriteLine("Niepoprawny wybór funkcji.");
                    return;
            }
        }

        private List<CustomArguments> GenerateArgs(Func<double, double> func)
        {
            List<CustomArguments> argumentsList = new List<CustomArguments>();

            CustomArguments arg1 = new CustomArguments(func, -10, 10, true);
            CustomArguments arg2 = new CustomArguments(func, -5, 20, true);
            CustomArguments arg3 = new CustomArguments(func, -5, 0, true);

            CustomArguments arg4 = new CustomArguments(func, -10, 10, false);
            CustomArguments arg5 = new CustomArguments(func, -5, 20, false);
            CustomArguments arg6 = new CustomArguments(func, -5, 0, false);

            argumentsList.Add(arg1);
            argumentsList.Add(arg2);
            argumentsList.Add(arg3);

            argumentsList.Add(arg4);
            argumentsList.Add(arg5);
            argumentsList.Add(arg6);
            argumentsList.Add(arg6);

            return argumentsList;
        }

        void Cancel()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.C)
                {
                    foreach (var worker in backgroundWorkers)
                    {
                        if (worker != null && worker.IsBusy)
                        {
                            worker.CancelAsync();
                        }
                    }
                }
            }
        }
    }

}