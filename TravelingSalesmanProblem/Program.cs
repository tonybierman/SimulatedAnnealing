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