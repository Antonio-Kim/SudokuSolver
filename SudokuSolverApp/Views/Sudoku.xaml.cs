namespace SudokuSolverApp.Views;

public partial class Sudoku : ContentPage
{
    private int[,] sudokuGrid = new int[9, 9]
        {
                { 5, 3, 0, 0, 7, 0, 0, 0, 0 },
                { 6, 0, 0, 1, 9, 5, 0, 0, 0 },
                { 0, 9, 8, 0, 0, 0, 0, 6, 0 },
                { 8, 0, 0, 0, 6, 0, 0, 0, 3 },
                { 4, 0, 0, 8, 0, 3, 0, 0, 1 },
                { 7, 0, 0, 0, 2, 0, 0, 0, 6 },
                { 0, 6, 0, 0, 0, 0, 2, 8, 0 },
                { 0, 0, 0, 4, 1, 9, 0, 0, 5 },
                { 0, 0, 0, 0, 8, 0, 0, 7, 9 }
        };
    public Sudoku()
    {
        InitializeComponent();
        CreateSudokuGrid();
    }

    private void CreateSudokuGrid()
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                var cellValue = sudokuGrid[row, col];

                // Create a Label for the cell value
                var cell = new Label
                {
                    Text = cellValue != 0 ? cellValue.ToString() : "",
                    BackgroundColor = Colors.White,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = 20,
                    TextColor = Colors.Black
                };
                Grid.SetRow(cell, row);
                Grid.SetColumn(cell, col);

                // Capture the current row and col in the closure
                int currentRow = row;
                int currentCol = col;

                // Add a tap gesture recognizer
                var tapGesture = new TapGestureRecognizer();
                tapGesture.Tapped += (s, e) => OnCellTapped(s, e, currentRow, currentCol, cell);
                cell.GestureRecognizers.Add(tapGesture);

                MainGrid.Children.Add(cell);
            }
        }
    }

    private async void OnCellTapped(object sender, EventArgs e, int row, int col, Label cell)
    {
        string result = await DisplayPromptAsync("Edit Cell", "Enter a new value:", initialValue: cell.Text, keyboard: Keyboard.Numeric);

        if (int.TryParse(result, out int newValue) && newValue >= 1 && newValue <= 9)
        {
            // Update the Label
            cell.Text = newValue.ToString();

            // Update the underlying int[,] array
            sudokuGrid[row, col] = newValue;

            // Debug message
            await DisplayAlert("Debug Info", $"You've entered {result} on row {row}, column {col}", "OK");
        }
        else if (string.IsNullOrEmpty(result))
        {
            // Clear the cell
            cell.Text = "";
            sudokuGrid[row, col] = 0;

            // Debug message
            await DisplayAlert("Debug Info", $"Cleared value on row {row}, column {col}", "OK");
        }
    }

}