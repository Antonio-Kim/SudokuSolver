using FluentAssertions;
using Solver.Utils;
using SudokuSolver.Solver.Models;
using SudokuSolver.Solver.Utils;

namespace SudokuSolver.TestSolver;

public class AlgorithmsTest
{
    [Fact]
    public void Backtracking_UnsolvedEasy_ReturnsSolvedPuzzle()
    {
        // Arrange
        Grid sut = UnsolvedEasyPuzzleOne();
        Grid answer = SolvedEasyPuzzleOne();

        // Act
        var result = Algorithms.Backtracking(sut);

        // Assert
        result.table.Should().BeEquivalentTo(answer.table);
    }

    private Grid UnsolvedEasyPuzzleOne()
    {
        Grid grid = new Grid()
        {
            table = new int?[][]
            {
                new int?[] {0,6,0,3,0,0,0,5,8},
                new int?[] {0,4,3,6,8,0,0,1,0},
                new int?[] {0,0,0,9,1,7,3,6,0},
                new int?[] {0,9,4,0,7,3,0,0,1},
                new int?[] {3,0,6,0,0,8,4,0,0},
                new int?[] {2,0,8,0,6,0,5,0,0},
                new int?[] {1,0,0,8,0,0,7,0,9},
                new int?[] {6,0,0,0,3,0,1,0,5},
                new int?[] {0,2,9,7,0,0,0,0,0}
            },
        };

        return grid;
    }

    private Grid SolvedEasyPuzzleOne()
    {
        Grid grid = new Grid()
        {
            table = new int?[][]
            {
                new int?[] {7,6,1,3,4,2,9,5,8},
                new int?[] {9,4,3,6,8,5,2,1,7},
                new int?[] {8,5,2,9,1,7,3,6,4},
                new int?[] {5,9,4,2,7,3,6,8,1},
                new int?[] {3,1,6,5,9,8,4,7,2},
                new int?[] {2,7,8,1,6,4,5,9,3},
                new int?[] {1,3,5,8,2,6,7,4,9},
                new int?[] {6,8,7,4,3,9,1,2,5},
                new int?[] {4,2,9,7,5,1,8,3,6}
            },
        };

        return grid;
    }
}