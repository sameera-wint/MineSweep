using MineSweeper;

Console.Write(Resource.WelcomeMessage);

#region Validate Grid size & Mines count
int size;
var minSize = 3;
var maxSize = 10;

while (true)
{
    Console.Write(Resource.GridInputHelpText);
    var sizeInput = Console.ReadLine();
    _ = int.TryParse(sizeInput, out size);

    if (sizeInput == null || !sizeInput.All(char.IsNumber))
    {
        Console.Write(Resource.IncorrectInput);
    }
    else if (size < minSize)
    {
        Console.Write(Resource.GridMinValidation, minSize);
    }
    else if (size > maxSize)
    {
        Console.Write(Resource.GridMaxValidation, maxSize);
    }
    else if (size >= 3 && size <= 10)
    {
        break;
    }
}

int mines;
var minMines = 1;
var maxMines = (int)(size * size * 0.35); // Maximum 35% of cells can be mines
while (true)
{
    Console.Write(Resource.MinesInputHelpText);
    
    var mineInput = Console.ReadLine();
    _ = int.TryParse(mineInput, out mines);

    if (mineInput == null || !mineInput.All(char.IsNumber))
    {
        Console.Write(Resource.IncorrectInput);
    }
    else if (mines < minMines)
    {
        Console.Write(Resource.MinesMinValidation);
    }
    else if (mines > maxMines)
    {
        Console.Write(Resource.MinesMaxValidation);
    }
    else if (mines >= minMines && mines <= maxMines)
    {
        break;
    }
}
#endregion

char[,] grid = MineSweeper.MineSweeper.CreateGrid(size, mines);

int uncoveredCount = 0;
int totalCount = size * size - mines;
bool[,] visited = new bool[size, size];

while (true)
{
    MineSweeper.MineSweeper.PrintGrid(grid);

    Console.Write(Resource.SelectCellText);
    var cell = Console.ReadLine();
    if (cell?.Length != 2 || !char.IsLetter(cell[0]) || !char.IsNumber(cell[1])) 
    {
        Console.WriteLine(Resource.IncorrectInput);
        continue;
    }

    int x = cell[0] - 65;
    int y = int.Parse(cell[1].ToString())-1;
    if (x >= size || y >= size)
    {
        Console.WriteLine(Resource.IncorrectInput);
        continue;
    }

    if (grid[x, y] == 'X')
    {
        Console.WriteLine(Resource.GameOverText);
        MineSweeper.MineSweeper.PrintGrid(grid, true);
        break;
    }
    if (!visited[x, y])
    {
        MineSweeper.MineSweeper.Uncover(grid, x, y, visited);
        uncoveredCount++;
    }
    if (uncoveredCount == totalCount)
    {
        Console.WriteLine(Resource.GameWonText);
        MineSweeper.MineSweeper.PrintGrid(grid, true);
        break;
    }
}
