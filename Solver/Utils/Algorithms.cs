using SudokuSolver.Solver.Models;
using SudokuSolver.Solver.Utils;

namespace Solver.Utils;

public static class Algorithms
{
	public static Grid Backtracking(Grid grid)
	{
		if (solve(grid, 0, 0))
		{
			return grid;
		}
		else
		{
			return null;
		}
	}

	private static bool solve(Grid grid, int row, int col)
	{
		if (col == Grid.column)
		{
			col = 0;
			row++;
			if (row == Grid.row)
			{
				return true;
			}
		}

		if (grid.table[row][col] != null)
		{
			return solve(grid, row, col + 1);
		}

		for (int num = 1; num <= 9; num++)
		{
			if (Validation.validInput(grid, row, col, num))
			{
				grid.table[row][col] = num;
				if (solve(grid, row, col + 1))
					return true;
				grid.table[row][col] = null;
			}
		}

		return false;
	}
}
