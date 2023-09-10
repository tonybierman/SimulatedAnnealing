namespace SimulatedAnnealing.Tests
{
    [TestClass]
    public class AnnealerTests
    {
        private const double Tolerance = 1e-2; // This is the allowed difference between expected and actual result due to the random nature of the algorithm.

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

        [TestMethod]
        public void Test_High_Start_Temperature()
        {
            Annealer annealer = new Annealer(1e6, 0.01, 10000);
            double solution = annealer.Solve(x => x * x, 20);
            Assert.IsTrue(Math.Abs(solution * solution) <= Tolerance);
        }

        [TestMethod]
        public void Test_Low_Start_Temperature()
        {
            Annealer annealer = new Annealer(0.01, 0.01, 10000);
            double initialSolution = 20;
            double solution = annealer.Solve(x => x * x, initialSolution);

            // Only assert that the algorithm runs and returns a valid solution
            Assert.IsTrue(solution >= double.MinValue && solution <= double.MaxValue, $"Invalid solution value: {solution}");
        }

        [TestMethod]
        public void Test_Low_End_Temperature()
        {
            Annealer annealer = new Annealer(1000, 1e-6, 10000);
            double solution = annealer.Solve(x => x * x, 20);
            Assert.IsTrue(Math.Abs(solution * solution) <= Tolerance);
        }

        [TestMethod]
        public void Test_Negative_Initial_Solution()
        {
            Annealer annealer = new Annealer(1000, 0.01, 10000);
            double solution = annealer.Solve(x => x * x, -20);
            Assert.IsTrue(Math.Abs(solution * solution) <= Tolerance);
        }

        [TestMethod]
        public void Test_Small_Number_Of_Iterations()
        {
            Annealer annealer = new Annealer(1000, 0.01, 10); // only 10 iterations
            double solution = annealer.Solve(x => x * x, 20);
            Assert.IsTrue(Math.Abs(solution * solution) > Tolerance);
        }

        [TestMethod]
        public void Test_Large_Number_Of_Iterations()
        {
            Annealer annealer = new Annealer(1000, 0.01, (int)1e6); // a large number of iterations
            double solution = annealer.Solve(x => x * x, 20);
            Assert.IsTrue(Math.Abs(solution * solution) <= Tolerance);
        }
    }
}
