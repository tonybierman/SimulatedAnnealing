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
