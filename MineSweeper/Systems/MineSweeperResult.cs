namespace MineSweeper
{
    public class MineSweeperResult<T>
    {
        public bool IsSuccess { get; set; }
        public string[]? ValidationMessages { get; set; }
        public T? Result { get; set; }

        public static MineSweeperResult<T> Error(string error)
        {
            return new MineSweeperResult<T> { IsSuccess = false, ValidationMessages = new string[] { error } };
        }

        public static MineSweeperResult<T> Success(T value)
        {
            return new MineSweeperResult<T> { IsSuccess = true, Result = value };
        }
    }
}
