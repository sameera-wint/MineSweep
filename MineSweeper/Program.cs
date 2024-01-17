using MineSweeper.Constants;
using MineSweeper.Resources;
using MineSweeperSystem = MineSweeper.Systems.MineSweeper;

Console.WriteLine(Resource.WelcomeMessage);

#region Validate Grid size & Mines count
int size;
var minSize = MineSweeperConstants.MinGridSize;
var maxSize = MineSweeperConstants.MaxGridSize;

while (true)
{
    Console.Write(Resource.GridInputHelpText);
    var sizeInput = Console.ReadLine();
    _ = int.TryParse(sizeInput, out size);

    if (sizeInput == null || !sizeInput.All(char.IsNumber))
    {
        Console.WriteLine(Resource.IncorrectInput);
    }
    else if (size < minSize)
    {
        Console.WriteLine(Resource.GridMinValidation, minSize);
    }
    else if (size > maxSize)
    {
        Console.WriteLine(Resource.GridMaxValidation, maxSize);
    }
    else if (size >= minSize && size <= maxSize)
    {
        break;
    }
}

int mines;
var minMines = 1;
var maxMines = (int)(size * size * (MineSweeperConstants.MinesPercentageMax / 100));
while (true)
{
    Console.Write(Resource.MinesInputHelpText);
    
    var mineInput = Console.ReadLine();
    _ = int.TryParse(mineInput, out mines);

    if (mineInput == null || !mineInput.All(char.IsNumber))
    {
        Console.WriteLine(Resource.IncorrectInput);
    }
    else if (mines < minMines)
    {
        Console.WriteLine(Resource.MinesMinValidation);
    }
    else if (mines > maxMines)
    {
        Console.WriteLine(Resource.MinesMaxValidation);
    }
    else if (mines >= minMines && mines <= maxMines)
    {
        break;
    }
}
#endregion

char[,] grid = MineSweeperSystem.CreateGrid(size, mines);

var uncoveredCount = 0;
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

    if (grid[x, y] == MineSweeperConstants.MineSymbol)
    {
        Console.WriteLine(Resource.GameOverText);
        MineSweeperSystem.PrintGrid(grid, true);
        break;
    }
    if (!visited[x, y])
    {
        MineSweeperSystem.Uncover(grid, x, y, visited);
        uncoveredCount++;
    }
    if (uncoveredCount == totalCount)
    {
        Console.WriteLine(Resource.GameWonText);
        MineSweeperSystem.PrintGrid(grid, true);
        break;
    }
}
