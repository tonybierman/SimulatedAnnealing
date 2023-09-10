using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatedAnnealing
{
    /// <summary>
    /// Represents an implementation of the Simulated Annealing optimization algorithm. 
    /// Simulated Annealing is a probabilistic technique used for finding an approximate 
    /// solution to an optimization problem. The algorithm is inspired by the annealing 
    /// process in metallurgy, where material is heated and then cooled to remove defects, 
    /// leading to a more optimized structure.
    /// </summary>
    /// <remarks>
    /// Key Features:
    /// 1. Temperature Schedule: The class begins with an initial high temperature, which 
    ///    allows for a higher probability of accepting worse solutions. Over time, this 
    ///    temperature is reduced, decreasing the chance of accepting sub-optimal solutions. 
    ///    This behavior aids the algorithm in exploring the solution space initially and 
    ///    then refining the search as it progresses.
    /// 2. Neighbor Generation: For a given solution, the algorithm produces a neighbor 
    ///    solution with slight variations. This is done to explore the surrounding solution space.
    /// 3. Acceptance Criteria: The decision to move from one solution to a neighbor is not 
    ///    just based on improvement in the objective function but also on the current temperature 
    ///    and the difference between the current and new solution's objective values.
    /// 4. Flexibility: The class can be used for various optimization problems by providing 
    ///    appropriate objective functions. It has parameters to control the starting and ending 
    ///    temperatures, as well as the number of iterations, allowing users to customize the 
    ///    annealing process according to their specific problem.
    /// 
    /// Usage:
    /// This class is especially useful for optimization problems where the solution space has 
    /// multiple local optima, and there's a risk of a greedy algorithm getting stuck in a sub-optimal 
    /// solution. By introducing a probabilistic acceptance criteria, Simulated Annealing can 
    /// potentially escape local optima and explore other regions of the solution space, increasing 
    /// the chance of finding or getting closer to the global optimum.
    /// </remarks>
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
