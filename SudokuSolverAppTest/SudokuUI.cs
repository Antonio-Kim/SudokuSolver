using Solver.Utils;
using SudokuSolver.Solver.Models;

namespace SudokuSolver.SudokuSolverAppTest
{
    public class SudokuUI
    {
        private GridService _gridService;

        public SudokuUI()
        {
            _gridService = new GridService();
        }

        public void Start()
        {
            Console.WriteLine("Enter the Sudoku grid:");

            int[][] gridValues = ReadSudokuGridFromConsole();

            // Set the grid values in GridService
            SetGridValues(gridValues);

            _gridService.SolveTable();
            // Display the grid (optional)
            DisplayGrid();
        }

        private int[][] ReadSudokuGridFromConsole()
        {
            int[][] gridValues = new int[Grid.row][];

            Console.WriteLine($"Enter values for each rows (separated by spaces):");
            for (int i = 0; i < Grid.row; i++)
            {
                string input = Console.ReadLine();
                string[] values = input.Split(' ');

                gridValues[i] = new int[Grid.column];
                for (int j = 0; j < Grid.column; j++)
                {
                    if (int.TryParse(values[j], out int num))
                    {
                        gridValues[i][j] = num;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid input: {values[j]} is not a valid number.");
                        // You can handle this case as per your application's logic (e.g., retry input)
                    }
                }
            }

            return gridValues;
        }

        private void SetGridValues(int[][] gridValues)
        {
            Grid grid = new Grid();
            grid.table = new int?[Grid.row][];

            for (int i = 0; i < Grid.row; i++)
            {
                grid.table[i] = new int?[Grid.column];
                for (int j = 0; j < Grid.column; j++)
                {
                    grid.table[i][j] = gridValues[i][j];
                }
            }

            _gridService.SetTable(grid);
        }

        private void DisplayGrid()
        {
            Grid grid = _gridService.GetTable();

            Console.WriteLine("\nSudoku Grid:");
            for (int i = 0; i < Grid.row; i++)
            {
                for (int j = 0; j < Grid.column; j++)
                {
                    Console.Write(grid.table[i][j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
