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
namespace SimulatedAnnealing.Tests
{
    [TestClass]
    public class AnnealerTests
    {
        private const double Tolerance = 1e-2; // This is the allowed difference between expected and actual result due to the random nature of the algorithm.

        /// <summary>
        /// Tests if the Annealer can find the minimum value (which is 0) for a simple quadratic function.
        /// </summary>
        [TestMethod]
        public void Test_Annealer_Minimum_Of_Quadratic_Function()
        {
            // Arrange
            Annealer annealer = new Annealer(1000, 0.01, 10000);
            Func<double, double> quadraticFunction = x => x * x;

            // Act
            double solution = annealer.Solve(quadraticFunction, 20);

            // Assert
            double expectedMinimum = 0.0;
            Assert.IsTrue(Math.Abs(solution * solution - expectedMinimum) <= Tolerance, $"Expected a value near {expectedMinimum} but got {solution * solution} instead.");
        }

        /// <summary>
        /// Tests the Annealer's ability to find the minimum with a very high start temperature.
        /// </summary>
        [TestMethod]
        public void Test_High_Start_Temperature()
        {
            Annealer annealer = new Annealer(1e6, 0.01, 10000);
            double solution = annealer.Solve(x => x * x, 20);
            Assert.IsTrue(Math.Abs(solution * solution) <= Tolerance);
        }

        /// <summary>
        /// Tests the Annealer's behavior with a low start temperature. This is expected to provide limited exploration.
        /// </summary>
        [TestMethod]
        public void Test_Low_Start_Temperature()
        {
            Annealer annealer = new Annealer(0.01, 0.01, 10000);
            double initialSolution = 20;
            double solution = annealer.Solve(x => x * x, initialSolution);

            // Only assert that the algorithm runs and returns a valid solution
            Assert.IsTrue(solution >= double.MinValue && solution <= double.MaxValue, $"Invalid solution value: {solution}");
        }

        /// <summary>
        /// Tests the Annealer's ability to refine its solution with a very low end temperature.
        /// </summary>
        [TestMethod]
        public void Test_Low_End_Temperature()
        {
            Annealer annealer = new Annealer(1000, 1e-6, 10000);
            double solution = annealer.Solve(x => x * x, 20);
            Assert.IsTrue(Math.Abs(solution * solution) <= Tolerance);
        }

        /// <summary>
        /// Tests the Annealer's behavior when provided with a negative initial solution.
        /// </summary>
        [TestMethod]
        public void Test_Negative_Initial_Solution()
        {
            Annealer annealer = new Annealer(1000, 0.01, 10000);
            double solution = annealer.Solve(x => x * x, -20);
            Assert.IsTrue(Math.Abs(solution * solution) <= Tolerance);
        }

        /// <summary>
        /// Tests the Annealer's behavior with a very small number of iterations, expecting it to possibly not find the optimal solution.
        /// </summary>
        [TestMethod]
        public void Test_Small_Number_Of_Iterations()
        {
            Annealer annealer = new Annealer(1000, 0.01, 10); // only 10 iterations
            double solution = annealer.Solve(x => x * x, 20);
            Assert.IsTrue(Math.Abs(solution * solution) > Tolerance);
        }

        /// <summary>
        /// Tests the Annealer's performance with a very large number of iterations, expecting it to get closer to the optimal solution.
        /// </summary>
        [TestMethod]
        public void Test_Large_Number_Of_Iterations()
        {
            Annealer annealer = new Annealer(1000, 0.01, (int)1e6); // a large number of iterations
            double solution = annealer.Solve(x => x * x, 20);
            Assert.IsTrue(Math.Abs(solution * solution) <= Tolerance);
        }
    }
}
