using FluentAssertions;
using Solver.Models;
using Solver.Utils;

namespace SudokuSolver.TestSolver;

public class ValidationTest
{
	[Fact]
	public void IsValid_CompletedGrid_ReturnsTrue()
	{
		// Arrange
		Grid sut = CreateCompletedGrid();

		// Act
		var result = Validation.isValid(sut);

		// Assert
		result.Should().BeTrue();
	}

	[Fact]
	public void IsValid_MissingValue_ReturnsFalse()
	{
		// Arrange
		Grid sut = MissingOneValueGrid();

		// Act
		var result = Validation.isValid(sut);

		// Assert
		result.Should().BeFalse();
	}

	[Theory]
	[MemberData(nameof(SetRows))]
	public void ValidateRow_CompletedGrid_ReturnsTrue(int row)
	{
		// Arrange
		Grid sut = CreateCompletedGrid();

		// Act
		var result = Validation.validateRow(sut, row);

		// Assert
		result.Should().BeTrue();
	}

	[Fact]
	public void ValidateRow_MissingValueAtRow5_ReturnsFalse()
	{
		// Arrange
		Grid sut = MissingOneValueGrid();
		int missingValueRow = 4;

		// Act
		var result = Validation.validateRow(sut, missingValueRow);

		// Assert
		result.Should().BeFalse();
	}

	[Theory]
	[MemberData(nameof(SetRows))]
	public void ValidateRow_EmptyGrid_ReturnsFalse(int row)
	{
		// Arrange
		Grid sut = EmptyGrid();

		// Act
		var result = Validation.validateRow(sut, row);

		// Assert
		result.Should().BeFalse();
	}

	[Theory]
	[MemberData(nameof(SetColumns))]
	public void ValidateColumn_CompletedGrid_ReturnsTrue(int column)
	{
		// Arrange 
		Grid sut = CreateCompletedGrid();

		// Act
		var result = Validation.validateColumn(sut, column);

		// Assert
		result.Should().BeTrue();
	}

	[Theory]
	[MemberData(nameof(SetColumns))]
	public void ValidateColumn_EmptyGrid_ReturnsFalse(int column)
	{
		// Arrange 
		Grid sut = EmptyGrid();

		// Act
		var result = Validation.validateColumn(sut, column);

		// Assert
		result.Should().BeFalse();
	}

	[Fact]
	public void ValidateColumn_MissingValue_ReturnsFalse()
	{
		// Arrange
		Grid sut = MissingOneValueGrid();
		int missingValueColumn = 2;

		// Act
		var result = Validation.validateColumn(sut, missingValueColumn);

		// Assert
		result.Should().BeFalse();
	}

	[Fact]
	public void ValidateSquare_CompletedGrid_ReturnsTrue()
	{
		// Arrange
		Grid sut = CreateCompletedGrid();

		// Act
		var result = Validation.validateSquares(sut);

		// Assert
		result.Should().BeTrue();
	}

	[Fact]
	public void ValidateSquare_IncorrectGrid_ReturnsFalse()
	{
		// Arrange
		Grid sut = IncorrectGrid();

		// Act
		var result = Validation.validateSquares(sut);

		// Assert
		result.Should().BeFalse();
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

	private Grid IncorrectGrid()
	{
		Grid grid = new Grid()
		{
            table = new int?[][]
            {
                new int?[] {7,6,1,3,4,2,9,5,8},
                new int?[] {9,6,3,6,8,5,2,1,7}, // row 1 col 1 has duplicate 6
                new int?[] {8,5,2,9,1,7,3,6,4},
                new int?[] {5,9,4,2,7,3,6,8,1},
                new int?[] {3,1,6,5,9,8,4,7,2},
                new int?[] {2,7,8,1,6,4,9,9,3}, // row 6 column 7 has duplicate 9
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
				new int?[] {3,1,null,5,9,8,4,7,2},
				new int?[] {2,7,8,1,6,4,5,9,3},
				new int?[] {1,3,5,8,2,6,7,4,9},
				new int?[] {6,8,7,4,3,9,1,2,5},
				new int?[] {4,2,9,7,5,1,8,3,6}
			},
		};

		return grid;
	}

	private Grid EmptyGrid()
	{
		return new Grid();
	}

	public static IEnumerable<object[]> SetRows()
	{
		for (int i = 0; i < 9; i++)
		{
			yield return new object[] { i };
		}
	}

	public static IEnumerable<object[]> SetColumns()
	{
		for (int j = 0; j < 9; j++)
		{
			yield return new object[] { j };
		}
	}
}