using MineSweeper.Constants;

namespace MineSweeper.Systems
{
    public class MineSweeper
    {
        public static int UncoveredCount { get; set; }

        public static char[,] CreateGrid(int size, int mines)
        {
            char[,] grid = new char[size, size];
            Random rand = new();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = ' ';
                }
            }

            int placedMines = 0;
            while (placedMines < mines)
            {
                int x = rand.Next(0, size);
                int y = rand.Next(0, size);

                if (grid[x, y] != MineSweeperConstants.MineSymbol)
                {
                    grid[x, y] = MineSweeperConstants.MineSymbol;
                    placedMines++;
                }
            }
            UncoveredCount = placedMines;

            return grid;
        }

        public static void PrintGrid(char[,] grid, bool showMines = false)
        {
            Console.WriteLine();
            int size = grid.GetLength(0);

            //printing column numbers
            Console.Write("  ");
            for (int j = 1; j <= size; j++)
            {
                Console.Write(j + " ");
            }
            Console.WriteLine();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    //printing row letters
                    if (j == 0)
                    {
                        Console.Write((char)(i + 65) + " ");
                    }
                    var cellText = grid[i, j] == MineSweeperConstants.MineSymbol && !showMines ? " " : grid[i, j].ToString();
                    Console.Write(cellText + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static void Uncover(char[,] grid, int x, int y, bool[,] visited)
        {
            if (x < 0 || x >= grid.GetLength(0) || y < 0 || y >= grid.GetLength(1) || visited[x, y])
            {
                return;
            }

            visited[x, y] = true;
            UncoveredCount++;

            if (grid[x, y] != ' ')
            {
                return;
            }

            int count = CountAdjacentMines(grid, x, y);
            grid[x, y] = count.ToString()[0];

            if (count == 0)
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        Uncover(grid, x + i, y + j, visited);
                    }
                }
            }
        }

        public static int CountAdjacentMines(char[,] grid, int x, int y)
        {
            int count = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (x + i >= 0 && x + i < grid.GetLength(0) && y + j >= 0 && y + j < grid.GetLength(1) && grid[x + i, y + j] == MineSweeperConstants.MineSymbol)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}
