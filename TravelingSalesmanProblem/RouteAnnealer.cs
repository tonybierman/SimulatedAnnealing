/*
 * Author: Tony Bierman
 * Website: http://www.tonybierman.com
 * License: MIT License
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy,
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software
 * is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
 * LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
 * IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesmanProblem
{
    public class RouteAnnealer
    {
        private static Random rnd = new Random();

        public double StartProbability { get; set; }
        public double EndProbability { get; set; }
        public int MaxIterations { get; set; }

        public RouteAnnealer(double startProbability, double endProbability, int maxIterations)
        {
            StartProbability = startProbability;
            EndProbability = endProbability;
            MaxIterations = maxIterations;
        }

        public List<City> Solve(Func<List<City>, double> objectiveFunction, List<City> startSolution)
        {
            List<City> currentSolution = new List<City>(startSolution);
            List<City> bestSolution = new List<City>(currentSolution);
            double bestObjective = objectiveFunction(bestSolution);

            for (int i = 0; i < MaxIterations; i++)
            {
                double probability = CurrentProbability(i);

                List<City> newSolution = Neighbour(currentSolution);
                double newObjective = objectiveFunction(newSolution);

                if (ShouldAccept(currentSolution, newSolution, probability, objectiveFunction))
                {
                    currentSolution = newSolution;

                    if (newObjective < bestObjective)
                    {
                        bestObjective = newObjective;
                        bestSolution = newSolution;
                    }
                }

                if (probability < EndProbability)
                    break;
            }

            return bestSolution;
        }

        private bool ShouldAccept(List<City> currentSolution, List<City> newSolution, double probability, Func<List<City>, double> objectiveFunction)
        {
            double currentEnergy = objectiveFunction(currentSolution);
            double newEnergy = objectiveFunction(newSolution);

            if (newEnergy < currentEnergy) return true;

            double acceptanceProbability = Math.Exp((currentEnergy - newEnergy) / probability);
            return rnd.NextDouble() < acceptanceProbability;
        }

        private List<City> Neighbour(List<City> currentSolution)
        {
            List<City> neighbourSolution = new List<City>(currentSolution);
            int index1 = rnd.Next(1, neighbourSolution.Count);
            int index2 = rnd.Next(1, neighbourSolution.Count);

            while (index1 == index2)
            {
                index2 = rnd.Next(1, neighbourSolution.Count);
            }

            City temp = neighbourSolution[index1];
            neighbourSolution[index1] = neighbourSolution[index2];
            neighbourSolution[index2] = temp;

            return neighbourSolution;
        }

        private double CurrentProbability(int iteration)
        {
            return StartProbability - (StartProbability - EndProbability) * (double)iteration / MaxIterations;
        }
    }
}
