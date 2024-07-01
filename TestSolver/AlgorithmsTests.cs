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
                new int?[] {null,6,null,3,null,null,null,5,8},
                new int?[] {null,4,3,6,8,null,null,1,null},
                new int?[] {null,null,null,9,1,7,3,6,null},
                new int?[] {null,9,4,null,7,3,null,null,1},
                new int?[] {3,null,6,null,null,8,4,null,null},
                new int?[] {2,null,8,null,6,null,5,null, null },
                new int?[] {1,null,null,8,null,null,7,null,9},
                new int?[] {6,null,null,null,3,null,1,null,5},
                new int?[] {null,2,9,7,null,null,null,null,null}
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