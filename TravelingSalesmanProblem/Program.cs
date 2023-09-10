namespace TravelingSalesmanProblem
{
    public class Program
    {
        private static Random rnd = new Random();

        public static void Main()
        {
            // Sample cities
            List<City> cities = new List<City>
            {
                new City { Id = 1, X = 0, Y = 0 },
                new City { Id = 2, X = 1, Y = 4 },
                new City { Id = 3, X = 3, Y = 4 },
                new City { Id = 4, X = 6, Y = 1 },
                new City { Id = 5, X = 4, Y = 2 }
            };

            Func<List<City>, double> objectiveFunction = path =>
            {
                double distance = 0;
                for (int i = 0; i < path.Count - 1; i++)
                {
                    distance += path[i].DistanceTo(path[i + 1]);
                }
                return distance;
            };

            RouteAnnealer annealer = new RouteAnnealer(1, 0.001, 10000);

            // Shuffling cities to get a random start solution
            List<City> shuffledCities = cities.OrderBy(x => rnd.Next()).ToList();
            List<City> solution = annealer.Solve(objectiveFunction, shuffledCities);

            Console.WriteLine("Optimized path:");
            foreach (City city in solution)
            {
                Console.WriteLine($"City {city.Id} ({city.X}, {city.Y})");
            }
            Console.WriteLine($"Total Distance: {objectiveFunction(solution)}");
        }
    }

}