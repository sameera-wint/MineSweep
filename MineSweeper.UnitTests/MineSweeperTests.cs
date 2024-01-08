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

        [Test]
        public void CreateGrid_ValidInput_GridCreatedWithMines()
        {
            // Arrange
            int size = 5;
            int mines = 5;

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
            Assert.AreEqual(mines, mineCount);
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

        [TestCase(5, 5)]
        [TestCase(8, 10)]
        [TestCase(10, 15)]
        public void GridCreation_Test(int size, int mines)
        {
            char[,] grid = MineSweeper.CreateGrid(size, mines);

            int mineCount = 0;
            int totalCells = size * size;
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

            Assert.Multiple(() =>
            {
                Assert.That(mineCount, Is.EqualTo(mines));
                Assert.That((totalCells - mineCount), Is.EqualTo(totalCells - mines));
            });
        }

    }
}