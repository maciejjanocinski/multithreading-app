using System;
using System.Collections.Generic;

public class CustomArguments
{
    public CustomArguments(Func<double, double> func, int a, int b, bool czyMetodaProstokatow)
    {
        this.func = func;
        this.a = a;
        this.b = b;
        this.czyMetodaProstokatow = czyMetodaProstokatow;
    }

    public Func<double, double> func { get; set; }
    public int a { get; set; }
    public int b { get; set; }

    public Boolean czyMetodaProstokatow;


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
}

