using System;
using System.Diagnostics;

namespace SimulatedAnnealing
{
    class Program
    {
        static void Main()
        {
            Annealer annealer = new Annealer(1000, 0.01, 10000);

            // Example problem: Find minimum value of the function f(x) = x^2
            double solution = annealer.Solve(x => x * x, 20);

            Console.WriteLine($"Approximate solution: {solution}");
            Console.WriteLine($"Objective function value at this solution: {solution * solution}");
        }
    }
}