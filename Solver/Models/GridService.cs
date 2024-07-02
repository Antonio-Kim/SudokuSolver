using Solver.Utils;

namespace SudokuSolver.Solver.Models;

public class GridService : IGridService
{
    private Grid _grid;

    public GridService()
    {
        _grid = new Grid();
        _grid.table = new int?[Grid.row][];
        for (int i = 0; i < Grid.row; i++)
        {
            _grid.table[i] = new int?[Grid.column];
            for (int j = 0; j < Grid.column; j++)
            {
                _grid.table[i][j] = 0;
            }
        }
    }
    public void DeleteCell(int row, int col)
    {
        if (row >= 0 && row < Grid.row && col >= 0  && col < Grid.column)
        {
            _grid.table[row][col] = 0;
        } else
        {
            throw new IndexOutOfRangeException("Row or column index is out of range");
        }
    }

    public int GetCell(int row, int col)
    {
        if (row >= 0 && row < Grid.row && col >= 0 && col < Grid.column)
        {
            int? cell = _grid.table[row][col];
            if (cell.HasValue)
                return cell.Value;
            else
                throw new InvalidOperationException("cell value is null");
        }
        else
        {
            throw new IndexOutOfRangeException("Row or column index is out of range");
        }
    }
    public Grid GetTable()
    {
        return _grid;
    }

    public void SetTable(Grid grid)
    {
        if (grid == null || grid.table == null) 
            throw new ArgumentNullException("Grid is not assigned");

        for (int i = 0; i < Grid.column; i++)
        {
            if (grid.table.Length != Grid.row || grid.table[i].Length != Grid.column)
                throw new ArgumentException("Grid size does not match");
        }
        
        for (int i = 0; i < Grid.row; i++)
        {
            for (int j = 0; j < Grid.column; j++)
            {
                var inputCell = grid.table[i][j];
                if (!inputCell.HasValue)
                {
                    throw new ArgumentNullException("The value is null inside the table");
                }    
                if (inputCell.Value < 0 || inputCell.Value > 9)
                {
                    throw new ArgumentOutOfRangeException("The value inside the cell must be between 0 and 9");
                }
                UpdateCell(i, j, inputCell.Value);
            }
        }
    }

    public void SolveTable()
    {
        _grid = Algorithms.Backtracking(_grid);
    }

    public void UpdateCell(int row, int col, int num)
    {
        if (num < 0 || num > 9)
        {
            throw new ArgumentOutOfRangeException("Input must be between 1 and 9");
        }

        if (row >= 0 && row < Grid.row && col >= 0 && col < Grid.column)
        {
            _grid.table[row][col] = num;
        }
        else
        {
            throw new IndexOutOfRangeException("Row or Column is out of range.");
        }
    }
}

public interface IGridService
{
    public int GetCell(int row, int col);
    public void UpdateCell(int num, int row, int col);
    public void DeleteCell(int row, int col);
    public Grid GetTable();
    public void SetTable(Grid grid);
    public void SolveTable();
}