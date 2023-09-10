using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatedAnnealing
{
    public class Annealer
    {
        private static Random rnd = new Random();

        public double StartTemperature { get; set; }
        public double EndTemperature { get; set; }
        public int MaxIterations { get; set; }

        public Annealer(double startTemperature, double endTemperature, int maxIterations)
        {
            StartTemperature = startTemperature;
            EndTemperature = endTemperature;
            MaxIterations = maxIterations;
        }

        public double Solve(Func<double, double> objectiveFunction, double startSolution)
        {
            double currentSolution = startSolution;
            double bestSolution = currentSolution;
            double bestObjective = objectiveFunction(bestSolution);

            for (int i = 0; i < MaxIterations; i++)
            {
                double temp = Temperature(i);

                double newSolution = Neighbour(currentSolution);
                double newObjective = objectiveFunction(newSolution);

                if (ShouldAccept(currentSolution, newSolution, temp, objectiveFunction))
                {
                    currentSolution = newSolution;

                    if (newObjective < bestObjective)
                    {
                        bestObjective = newObjective;
                        bestSolution = newSolution;
                    }
                }

                if (temp < EndTemperature)
                    break;
            }

            return bestSolution;
        }

        private bool ShouldAccept(double currentSolution, double newSolution, double temperature, Func<double, double> objectiveFunction)
        {
            double currentEnergy = objectiveFunction(currentSolution);
            double newEnergy = objectiveFunction(newSolution);

            // If new solution is better, accept it
            if (newEnergy < currentEnergy) return true;

            // If new solution is worse, calculate an acceptance probability and accept it based on that probability
            double acceptanceProbability = Math.Exp((currentEnergy - newEnergy) / temperature);
            return rnd.NextDouble() < acceptanceProbability;
        }

        private double Neighbour(double currentSolution)
        {
            // This method generates a neighbouring solution based on the current solution.
            // The exact implementation of this method will depend on the specific problem you're trying to solve.
            // For this example, I'm assuming that the solution is a real number and we just add/subtract a small random value.
            double offset = (rnd.NextDouble() * 2 - 1); // Random value between -1 and 1
            return currentSolution + offset;
        }

        private double Temperature(int iteration)
        {
            // Linear decay of temperature
            return StartTemperature - (StartTemperature - EndTemperature) * (double)iteration / MaxIterations;
        }
    }
}
