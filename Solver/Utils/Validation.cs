using SudokuSolver.Solver.Models;

namespace SudokuSolver.Solver.Utils;

public static class Validation
{
	public static bool isValid(Grid grid)
	{
		if (grid == null)
			return false;

		bool checkedRows = validRows(grid);
		bool checkedCols = validColumns(grid);
		bool checkedSquare = validateSquares(grid);

		return checkedRows || checkedCols || checkedSquare;
	}

	public static bool validColumns(Grid grid)
	{
		bool IsValidColumn = false;
		for (int i = 0; i < 9; i++)
		{
			validateColumn(grid, i);
		}
		return IsValidColumn;
	}

	public static bool validRows(Grid grid)
	{
		bool IsValidRows = false;
		for (int i = 0; i < 9; i++)
		{
			validateRow(grid, i);
		}
		return IsValidRows;
	}

	public static bool validateColumn(Grid grid, int column)
	{
		if (grid?.table == null)
			return false;

		HashSet<int> values = new HashSet<int>();

		for (int i = 0; i < 9; i++)
		{
			int? cell = grid.table[i][column];
			if (cell == 0 || !values.Add(cell.Value))
				return false;
		}

		return values.Count == 9;
	}

	public static bool validateRow(Grid grid, int row)
	{
		if (grid?.table == null || grid.table[row] == null) return false;

		HashSet<int> values = new HashSet<int>();
		for (int j = 0; j < 9; j++)
		{
			int? cell = grid.table[row][j];
			if (cell == 0 || !values.Add(cell.Value))
				return false;
		}

		return values.Count == 9;
	}

	public static bool validateSquares(Grid grid)
	{
		for (int i = 0; i < 9; i += 3)
		{
			for (int j = 0; j < 9; j += 3)
			{
				if (!checkOneSquare(grid, i, j))
				{
					return false;
				}
			}
		}

		return true;
	}

	public static bool checkOneSquare(Grid grid, int row, int col)
	{
		if (grid.table == null) return false;

		HashSet<int> square = new HashSet<int>();

		int baseRow = (row / 3) * 3;
		int baseCol = (col / 3) * 3;

		for (int i = baseRow; i < baseRow + 3; i++)
		{
			for (int j = baseCol; j < baseCol + 3; j++)
			{
				int? cell = grid.table[i][j];
				if (grid.table[i][j] == 0 || !square.Add(cell.Value))
					return false;
			}
		}

		return square.Count == 9;
	}

	public static bool validInput(Grid grid, int row, int col, int num)
	{
		if (grid.table == null) return false;
		if (num < 1 || num > 9) return false;

		for (int i = 0; i < Grid.row; i++)
		{
			if (grid.table[i][col] == num)
				return false;
		}

		for (int j = 0; j < Grid.column; j++)
		{
			if (grid.table[row][j] == num)
				return false;
		}

		int baseRow = (row / 3) * 3;
		int baseCol = (col / 3) * 3;

		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				if (grid.table[baseRow + i][baseCol + j] == num)
					return false;
			}
		}

		return true;
	}
}