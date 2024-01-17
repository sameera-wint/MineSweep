using MineSweeper.Constants;
using MineSweeper.Resources;

namespace MineSweeper.Validations
{
    public class InputValidator : IInputValidation
    {
        public MineSweeperResult<int> GetValidateGridSize(string? gridSize)
        {
            var minSize = MineSweeperConstants.MinGridSize;
            var maxSize = MineSweeperConstants.MaxGridSize;

            _ = int.TryParse(gridSize, out int size);

            if (string.IsNullOrEmpty(gridSize) || !gridSize.All(char.IsNumber))
            {
                return MineSweeperResult<int>.Error(Resource.IncorrectInput);
            }
            else if (size < minSize)
            {
                return MineSweeperResult<int>.Error(string.Format(Resource.GridMinValidation, minSize));
            }
            else if (size > maxSize)
            {
                return MineSweeperResult<int>.Error(string.Format(Resource.GridMaxValidation, maxSize));
            }
            return MineSweeperResult<int>.Success(size);
        }

        public MineSweeperResult<int> GetValidateMinesCount(string minesCount, int gridSize)
        {
            var minMines = MineSweeperConstants.MinMinesCount;
            var maxMines = (int)(gridSize * gridSize * (MineSweeperConstants.MinesPercentageMax / 100));

            _ = int.TryParse(minesCount, out int mines);

            if (string.IsNullOrEmpty(minesCount) || !minesCount.All(char.IsNumber))
            {
                return MineSweeperResult<int>.Error(Resource.IncorrectInput);
            }
            else if (mines < minMines)
            {
                return MineSweeperResult<int>.Error(string.Format(Resource.MinesMinValidation, minMines));
            }
            else if (mines > maxMines)
            {
                return MineSweeperResult<int>.Error(string.Format(Resource.MinesMaxValidation, maxMines));
            }

            return MineSweeperResult<int>.Success(mines);
        }
    }
}
