using CommunityToolkit.Mvvm.ComponentModel;
using SudokuSolver.Solver.Models;
using System.Collections.ObjectModel;

namespace SudokuSolverApp.SudokuSolverApp.ViewModels;

public class SudokuViewMdoel : ObservableObject
{
    private readonly GridService _gridService;
    private SudokuSolver.Solver.Models.Grid _grid;

    public SudokuViewMdoel()
    {
        _gridService = new GridService();
        _grid = _gridService.GetTable();
    }

    private int?[][] _table;
    public int?[][] Table
    {
        get { return _table; }
        set { SetProperty(ref _table, value); }
    }

    private void RefreshTable()
    {
        Table = _gridService.GetTable()?.table;
    }

    private void SolveTable()
    {
        _gridService.SolveTable();
        RefreshTable();
    }

    public void UpdateCell(int row, int col, int num)
    {
        _gridService.UpdateCell(row, col, num);
        RefreshTable();
    }

    public void DeleteCell(int row, int col)
    {
        _gridService?.DeleteCell(row, col);
        RefreshTable();
    }
}