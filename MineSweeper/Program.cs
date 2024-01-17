using MineSweeper.Constants;
using MineSweeper.Resources;
using MineSweeper.Validations;
using MineSweeperSystem = MineSweeper.Systems.MineSweeper;

Console.WriteLine(Resource.WelcomeMessage);

#region Validate Grid size & Mines count
int size;
int mines;

var inputValidator = new InputValidator();

while (true)
{
    Console.Write(Resource.GridInputHelpText);
    var sizeInput = Console.ReadLine();

    var validationResult = inputValidator.GetValidateGridSize(sizeInput);

    if (validationResult.IsSuccess == false)
    {
        foreach ( var validationMessage in validationResult.ValidationMessages)
            Console.WriteLine(validationMessage);
        continue;
    }
    size = validationResult.Result;
    break;
}

while (true)
{
    Console.Write(Resource.MinesInputHelpText);
    var mineInput = Console.ReadLine();
    
    var validationResult = inputValidator.GetValidateMinesCount(mineInput, size);

    if (validationResult.IsSuccess == false)
    {
        foreach (var validationMessage in validationResult.ValidationMessages)
            Console.WriteLine(validationMessage);
        continue;
    }
    mines = validationResult.Result;
    break;
}
#endregion

char[,] grid = MineSweeperSystem.CreateGrid(size, mines);

var totalCount = size * size - mines;
bool[,] visited = new bool[size, size];

while (true)
{
    MineSweeperSystem.PrintGrid(grid);

    Console.Write(Resource.SelectCellText);
    var cell = Console.ReadLine();
    if (cell?.Length != 2 || !char.IsLetter(cell[0]) || !char.IsNumber(cell[1])) 
    {
        Console.WriteLine(Resource.IncorrectInput);
        continue;
    }

    int x = cell[0] - 65;
    int y = int.Parse(cell[1].ToString()) - 1;
    if (x >= size || y >= size)
    {
        Console.WriteLine(Resource.IncorrectInput);
        continue;
    }

    if (!visited[x, y])
    {
        MineSweeperSystem.Uncover(grid, x, y, visited);
    }

    if (grid[x, y] == MineSweeperConstants.MineSymbol)
    {
        Console.WriteLine(Resource.GameOverText);
        MineSweeperSystem.PrintGrid(grid, true);
        break;
    }
    if (MineSweeperSystem.UncoveredCount > totalCount)
    {
        Console.WriteLine(Resource.GameWonText);
        MineSweeperSystem.PrintGrid(grid, true);
        break;
    }
}
