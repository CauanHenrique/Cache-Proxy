// See https://aka.ms/new-console-template for more information

using C_Proxy;
using Calculator = C_Proxy.interfaces.Calculator;

Calculator calc = new ProxyCalculator();
Console.WriteLine(calc.sum(2, 1));
Console.WriteLine(calc.sum(2, 2));
Console.WriteLine(calc.sum(2, 3));
Console.WriteLine(calc.sum(2, 4));
Console.WriteLine(calc.sum(2, 3));
Console.WriteLine(calc.sum(2, 5));