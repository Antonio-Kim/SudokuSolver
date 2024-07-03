using System;

namespace SudokuSolver.SudokuSolverAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SudokuUI sudokuUI = new SudokuUI();
            sudokuUI.Start();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}