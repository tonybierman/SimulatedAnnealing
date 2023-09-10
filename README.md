# SimulatedAnnealing

Simulated annealing is a probabilistic technique used for approximating the global optimum of a given function. Here's a simple simulated annealing algorithm implemented in C#.

## Annealer Class Description

The `Annealer` class is an implementation of the Simulated Annealing optimization algorithm. This probabilistic technique is for finding an approximate solution to an optimization problem, drawing inspiration from the annealing process in metallurgy.

## Key Features

- **Temperature Schedule**: Starts with a high initial temperature, allowing acceptance of worse solutions. Over iterations, this temperature drops, refining the solution search.
  
- **Neighbor Generation**: Produces slightly varied neighbor solutions for exploration.

- **Acceptance Criteria**: Moves between solutions based on the current temperature and the difference in objective values, not just solution improvement.

- **Flexibility**: Suitable for various optimization problems. Customize the annealing process with control parameters.

## Usage

Ideal for optimization problems with multiple local optima. The probabilistic acceptance criteria allows it to escape local optima, potentially getting closer to the global optimum.

# Annealer Unit Tests

## Descriptions of the unit tests for the `Annealer` class.

### 1. Test_Annealer_Minimum_Of_Quadratic_Function
- **Description**: 
  - Tests if the `Annealer` can find the minimum value (which is 0) for a simple quadratic function.

### 2. Test_High_Start_Temperature
- **Description**: 
  - Tests the `Annealer`'s ability to find the minimum with a very high start temperature.

### 3. Test_Low_Start_Temperature
- **Description**: 
  - Tests the `Annealer`'s behavior with a low start temperature. This is expected to provide limited exploration.

### 4. Test_Low_End_Temperature
- **Description**: 
  - Tests the `Annealer`'s ability to refine its solution with a very low end temperature.

### 5. Test_Negative_Initial_Solution
- **Description**: 
  - Tests the `Annealer`'s behavior when provided with a negative initial solution.

### 6. Test_Small_Number_Of_Iterations
- **Description**: 
  - Tests the `Annealer`'s behavior with a very small number of iterations, expecting it to possibly not find the optimal solution.

### 7. Test_Large_Number_Of_Iterations
- **Description**: 
  - Tests the `Annealer`'s performance with a very large number of iterations, expecting it to get closer to the optimal solution.
