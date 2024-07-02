using FluentAssertions;
using SudokuSolver.Solver.Models;
using System;

namespace SudokuSolver.TestSolver;

public class GridServiceTests
{
    [Fact]
    public void GetTable_InitialService_ReturnsNullTables()
    {
        var sut = new GridService();
        var expected = EmptyGrid();

        var result = sut.GetTable();

        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GetCell_RowOutsideGrid_ThrowsOutOfBoundException()
    {
        var sut = new GridService();

        sut.Invoking(s => s.GetCell(10, 0))
            .Should().Throw<IndexOutOfRangeException>();
    }

    [Fact]
    public void GetCell_ColumnOutsideGrid_ThrowsOutOfBoundException()
    {
        var sut = new GridService();

        sut.Invoking(s => s.GetCell(0, 10))
            .Should().Throw<IndexOutOfRangeException>();
    }

    [Fact]
    public void UpdateCell_InputOutOfBound_ThrowsArgumentOutOfRangeException()
    {
        var sut = new GridService();

        sut.Invoking(s => s.UpdateCell(0, 0, 10))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void UpdateCell_RowOutOfBound_ThrowsOutOfBoundException()
    {
        var sut = new GridService();

        sut.Invoking(s => s.UpdateCell(0, 10, 1))
            .Should().Throw<IndexOutOfRangeException>();
    }

    [Fact]
    public void UpdateCell_ColumnOutOfBound_ThrowsOutOfBoundException()
    {
        var sut = new GridService();

        sut.Invoking(s => s.UpdateCell(10,0,1))
            .Should().Throw<IndexOutOfRangeException>();
    }

    [Fact]
    public void UpdateCell_InsertANumber_ShouldUpdateTable()
    {
        var sut = new GridService();
        var result = OnlyOneValueGrid();
        int row = 0;
        int column = 0;
        int value = 1;

        sut.UpdateCell(row, column, value);

        var actual = sut.GetTable();
        actual.Should().BeEquivalentTo(result);
    }

    [Fact]
    public void DeleteCell_ColumnOutOfBound_ThrowsOutOfBoundException()
    {
        var sut = new GridService();

        sut.Invoking(s=>s.DeleteCell(10,0))
            .Should().Throw<IndexOutOfRangeException>();
    }

    [Fact]
    public void DeleteCell_RowOutOfBound_ThrowsOutOfBoundException()
    {
        var sut = new GridService();

        sut.Invoking(s => s.DeleteCell(0,10))
            .Should().Throw<IndexOutOfRangeException>();
    }

    [Fact]
    public void DeleteCell_ValidCell_ShouldRemoveValue()
    {
        var sut = new GridService();
        int row = 0;
        int column = 0;
        var expected = EmptyGrid();

        sut.UpdateCell(row, column, 1);
        sut.DeleteCell(row, column);

        var result = sut.GetTable();
        result.Should().BeEquivalentTo(expected);

    }

    [Fact]
    public void SetTable_SendValidTable_ReturnSameTable()
    {
        var sut = new GridService();
        var expected = CreateCompletedGrid();

        sut.SetTable(expected);
        var result = sut.GetTable();

        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void SetTable_UnsolvedEasyTable_ReturnSameTable()
    {
        var sut = new GridService();
        var expected = UnsolvedEasyPuzzleOne();

        sut.SetTable(expected);
        var result = sut.GetTable();

        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void SetTable_ExtraColumn_ThrowsArgumentException()
    {
        var sut = new GridService();
        var errorTable = InvalidTable_ColumnTooLong();
        sut.Invoking(s => s.SetTable(errorTable))
            .Should().Throw<ArgumentException>();
    }

    [Fact]
    public void SetTable_ShortColumn_ThrowsArgumentException()
    {
        var sut = new GridService();
        var errorTable = InvalidTable_ColumnTooShort();
        sut.Invoking(s => s.SetTable(errorTable))
            .Should().Throw<ArgumentException>();
    }

    [Fact]
    public void SetTable_Null_ThrowsArgumentNullException()
    {
        var sut = new GridService();
        sut.Invoking(s => s.SetTable(null))
            .Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void SetTable_CellHasNull_ThrowsArgumentNullException()
    {
        var sut = new GridService();
        var errorTable = InvalidTable_NullValue();
        sut.Invoking(s => s.SetTable(errorTable))
            .Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void SetTable_CellValueOutOfRange_ThrowsArgumentOutOfRangeException()
    {
        var sut = new GridService();
        var errorTable = InvalidTable_OutOfRange();
        sut.Invoking(s => s.SetTable(errorTable))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void SolveTable_EasyPuzzleOne_ReturnsCorrectTable()
    {
        var sut = new GridService();
        var expected = SolvedEasyPuzzleOne();
        var inputTable = UnsolvedEasyPuzzleOne();
        sut.SetTable(inputTable);

        sut.SolveTable();
        var result = sut.GetTable();

        result.Should().BeEquivalentTo(expected);
    }

    private Grid EmptyGrid()
    {
        Grid empty = new Grid();
        empty.table = new int?[Grid.row][];
        for (int i = 0; i < Grid.row; i++)
        {
            empty.table[i] = new int?[Grid.column];
            for (int j = 0; j < Grid.column; j++)
            {
                empty.table[i][j] = 0;
            }
        }
        return empty;
    }

    private Grid CreateCompletedGrid()
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

    private Grid MissingOneValueGrid()
    {
        Grid grid = new Grid()
        {
            table = new int?[][]
            {
                new int?[] {7,6,1,3,4,2,9,5,8},
                new int?[] {9,4,3,6,8,5,2,1,7},
                new int?[] {8,5,2,9,1,7,3,6,4},
                new int?[] {5,9,4,2,7,3,6,8,1},
                new int?[] {3,1,0,5,9,8,4,7,2}, // on the third column here
                new int?[] {2,7,8,1,6,4,5,9,3},
                new int?[] {1,3,5,8,2,6,7,4,9},
                new int?[] {6,8,7,4,3,9,1,2,5},
                new int?[] {4,2,9,7,5,1,8,3,6}
            },
        };

        return grid;
    }

    private Grid OnlyOneValueGrid()
    {
        Grid grid = new Grid()
        {
            table = new int?[][]
            {
                new int?[] {1,0,0,0,0,0,0,0,0},
                new int?[] {0,0,0,0,0,0,0,0,0},
                new int?[] {0,0,0,0,0,0,0,0,0},
                new int?[] {0,0,0,0,0,0,0,0,0},
                new int?[] {0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
                new int?[] {0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new int?[] {0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new int?[] {0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new int?[] {0,0,0,0,0,0,0,0,0}
            },
        };

        return grid;
    }

    private Grid InvalidTable_ColumnTooLong()
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
                new int?[] {1,3,5,8,2,6,7,4,9,7},
                new int?[] {6,8,7,4,3,9,1,2,5},
                new int?[] {4,2,9,7,5,1,8,3,6}
            },
        };

        return grid;
    }

    private Grid InvalidTable_ColumnTooShort()
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
                new int?[] {1,3,5,8,2,6,7,4},
                new int?[] {6,8,7,4,3,9,1,2,5},
                new int?[] {4,2,9,7,5,1,8,3,6}
            },
        };

        return grid;
    }

    private Grid InvalidTable_NullValue()
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
                new int?[] {2,7,8,1,null,4,5,9,3},
                new int?[] {1,3,5,8,2,6,7,4,9},
                new int?[] {6,8,7,4,3,9,1,2,5},
                new int?[] {4,2,9,7,5,1,8,3,6}
            },
        };

        return grid;
    }

    private Grid InvalidTable_OutOfRange()
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
                new int?[] {2,7,8,1,10,4,5,9,3},
                new int?[] {1,3,5,8,2,6,7,4,9},
                new int?[] {6,8,7,4,3,9,1,2,5},
                new int?[] {4,2,9,7,5,1,8,3,6}
            },
        };

        return grid;
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