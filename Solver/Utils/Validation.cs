using System.Windows.Markup;
using Solver.Models;

namespace Solver.Utils;

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
			if (cell == null || !values.Add(cell.Value))
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
			if (cell == null || !values.Add(cell.Value))
				return false;
		}

		return values.Count == 9;
	}

	public static bool validateSquares(Grid grid)
	{
		bool squareIsValid = true;
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

	private static bool checkOneSquare(Grid grid, int row, int col)
	{
		if (grid.table == null) return false;

		HashSet<int> square = new HashSet<int>();

		for (int i = row; i < row + 3; i++)
		{
			for (int j = col; j < col + 3; j++)
			{

				int? cell = grid.table[i][j];
				if (cell == null || !square.Add(cell.Value))
					return false;
			}
		}

		return square.Count == 9;
	}
}