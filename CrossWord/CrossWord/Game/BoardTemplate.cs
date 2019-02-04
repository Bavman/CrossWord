namespace CrossWord.Game
{
    public class BoardTemplate<T>
    {

        public T[,] Layout { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        public T EmptyChar { get; set; }

        public BoardTemplate(int width, int height, T emptyChar)
        {
            Width = width;
            Height = height;
            EmptyChar = emptyChar;

            Layout = InitializeBoard(width, height, emptyChar);
        }

        // Initializes board
        private T[,] InitializeBoard(int width, int height, T emptyChar)
        {

            var crossWordBoard = new T[height, width];

            for (var i = 0; i < height; i++)
            {

                for (var j = 0; j < width; j++)
                {
                    crossWordBoard[i, j] = emptyChar;
                }

            }


            return crossWordBoard;
        }
    }
}
