namespace MineSweeper.Validations
{
    public interface IInputValidation
    {
        MineSweeperResult<int> GetValidateGridSize(string gridSize);
        MineSweeperResult<int> GetValidateMinesCount(string minesCount, int gridSize);
    }
}
