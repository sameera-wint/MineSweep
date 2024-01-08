namespace MineSweeper.UnitTests
{
    public class MineSweeperTests
    {
        public MineSweeper MineSweeper { get; set; }

        [SetUp]
        public void Setup()
        {
            MineSweeper = new MineSweeper();
        }

        [TestCase(5, 5)]
        [TestCase(8, 10)]
        [TestCase(10, 15)]
        public void CreateGrid_ValidInput_GridCreatedWithMines(int size, int mines)
        {
            // Act
            char[,] grid = MineSweeper.CreateGrid(size, mines);

            // Assert
            int mineCount = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (grid[i, j] == 'X')
                    {
                        mineCount++;
                    }
                }
            }
            Assert.That(mineCount, Is.EqualTo(mines));
        }

        [Test]
        public void CountAdjacentMines_SurroundedByMines_ReturnsCorrectCount()
        {
            // Arrange
            char[,] grid = new char[,]
            {
            { 'X', 'X', 'X' },
            { 'X', ' ', 'X' },
            { 'X', 'X', 'X' }
            };

            // Act
            int count = MineSweeper.CountAdjacentMines(grid, 1, 1);

            // Assert
            Assert.That(count, Is.EqualTo(8));
        }

        [Test]
        public void Uncover_Test()
        {
            char[,] grid = new char[,]
            {
            {' ', 'X', ' '},
            {'X', 'X', ' '},
            {' ', ' ', 'X'}
            };

            bool[,] visited = new bool[,]
            {
            {false, false, false},
            {false, false, false},
            {false, false, false}
            };

            MineSweeper.Uncover(grid, 0, 0, visited);

            Assert.That(grid[1, 0], Is.EqualTo('X'));
            Assert.That(grid[2, 0], Is.EqualTo(' '));
            Assert.That(grid[0, 1], Is.EqualTo('X'));
            Assert.That(grid[1, 1], Is.EqualTo('X'));
            Assert.That(grid[2, 1], Is.EqualTo(' '));
        }
    }
}