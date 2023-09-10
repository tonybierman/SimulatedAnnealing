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