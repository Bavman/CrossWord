using CrossWord.GameTemplates;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace CrossWord.Game
{
    public class CrosswordBoardMain
    {
        #region Singleton Setup
        static CrosswordBoardMain _instance = null;

        private CrosswordBoardMain()
        {

        }

        public static CrosswordBoardMain Instance()
        {
            if (_instance == null)
            {
                _instance = new CrosswordBoardMain();
            }

            return _instance;
        }

        #endregion

        private char _emptyChar;
        public int Width;
        public int Height;

        public BoardTemplate<char> Board;
        public BoardTemplate<char> CrossWordBoardCheck;


        public void InitializeBoard(int width, int height, char emptyChar)
        {

            Board = new BoardTemplate<char>(height, width, emptyChar); // Array 1 is Row, Array 2 is Column
            CrossWordBoardCheck = new BoardTemplate<char>(height, width, '\0'); // Array 1 is Row, Array 2 is Column

            Width = Board.Width;
            Height = Board.Height;
            _emptyChar = Board.EmptyChar;
        }

        // Resets the board applying the emptyChar to all array items
        public void ResetBoard()
        {
            
            for (var i = 0; i < Height; i++)
            {

                for (var j = 0; j < Width; j++)
                {
                    Board.Layout[i, j] = _emptyChar;

                }

            }

        }

        // If word can be placed, it is
        public PlacedWord PlaceWord(string word, Tuple<int, int> startPos, WordDirection direction)
        {

            var canPlaceWord = CanWordBePlaced(word, startPos, direction);

            if (!canPlaceWord)
            {
                return null;
            }
            else
            {
                BruteForcePlaceWord(word, startPos, direction);
                return new PlacedWord { Word = word, StartPos = startPos, Direction = direction };
            }
        }

        // Is used to place word in grid arrays after checks have been done
        public void BruteForcePlaceWord(string word, Tuple<int, int> startPos, WordDirection direction)
        {
            var horizontalStart = startPos.Item1;
            var verticalStart = startPos.Item2;

            if (direction == WordDirection.Horizontal)
            {
                for (var i = horizontalStart; i < horizontalStart + word.Length; i++)
                {
                    Board.Layout[startPos.Item2,i] = word[i - horizontalStart];
                }
            }

            if (direction == WordDirection.Vertical)
            {
                for (var i = verticalStart; i < verticalStart + word.Length; i++)
                {
                    Board.Layout[i,startPos.Item1] = word[i - verticalStart];
                }
            }
        }


        // Returns the word start positions
        public List<Tuple<int,int>> GetWordStartPositions(string word, int charIndex, WordDirection direction)
        {
            var potentialStartPositions = new List<Tuple<int, int>>();

            var wordLength = word.Length;

            if (charIndex < 0 || charIndex > wordLength)
            {
                return potentialStartPositions;
            }

            var letter = word[charIndex];
            
            // Scan whole board
            for (var i = 0; i < Board.Layout.GetLength(0); i++)
            {

                for (var j = 0; j < Board.Layout.GetLength(1); j++)
                {

                    // Find matching char (letter)
                    if (Board.Layout[i,j] == letter)
                    {

                        if (direction == WordDirection.Horizontal)
                        {
                            // Horizontal Check if word is not too long and start of word fits on board
                            if ((wordLength - charIndex) + j <= Width && j - charIndex >= 0)
                            {

                                potentialStartPositions.Add(new Tuple<int, int>(j - charIndex, i));

                            }
                        }

                        if (direction == WordDirection.Vertical)
                        {
                            // Vertical Check if word is not too long and start of word fits on board
                            if ((wordLength - charIndex) + i <= Height && i - charIndex >= 0)
                            {

                                potentialStartPositions.Add(new Tuple<int, int>(j, i - charIndex));

                            }
                        }
                    }   
                }
            }

            return potentialStartPositions;
        }


        // Check if word can be placed by doing horizontal and vertical checks
        public bool CanWordBePlaced(string word, Tuple<int, int> startPos, WordDirection direction)
        {

            if (startPos.Item1 < 0 || startPos.Item2 < 0)
            {
                return false;
            }

            var wordLength = word.Length;

            // Scan word row and rows either side
            switch (direction)
            {
                case WordDirection.Horizontal:

                    if (startPos.Item1 + wordLength > Width || startPos.Item2 >= Height)
                    {
                        return false;
                    }
                    // Row (Actual Word)
                    if (HorizontalWordScan(startPos, wordLength + 1, word, direction))
                    {
                        return false;
                    }

                break;

                case WordDirection.Vertical:

                    if (startPos.Item2 + wordLength > Height || startPos.Item1 >= Width)
                    {
                        return false;
                    }

                    // Column (Actual Word)
                    if (VerticalWordScan(startPos, wordLength + 1, word, direction))
                    {
                        return false;
                    }

                break;

            }

            return true;
        }

        // Scan individual cells horizontally and returns true if any cells are not empty, excludes the ignore cell
        private bool HorizontalWordScan(Tuple<int, int> startPos, int length, string word, WordDirection direction)
        {
            
            var start = startPos.Item1;
            var end = start + length;

            for (var i = start; i < end; i++)
            {
                // Ignore if scan hits out of range cells
                if (i >= 0 && i < Width) 
                {
                    if (CheckHorizontalIntersectionsAndPadding(i, startPos, word))
                    {
                        return true;
                    }
                    
                }

            }

            return false;
        }

        // Scan cells vertically and returns true if any cells are not empty, excludes the ignore cell
        private bool VerticalWordScan(Tuple<int, int> startPos, int length, string word, WordDirection direction)
        {
            var start = startPos.Item2;
            var end = start + length;

            for (var i = start; i < end; i++)
            {

                // Ignore if scan hits out of range cells
                if (i >= 0 && i < Height)
                {
                    if (CheckVerticalIntersectionsAndPadding(i, startPos, word))
                    {
                        return true;
                    }
                    
                }

            }
            return false;
        }

        // Checks Horizontal Intersections
        private bool CheckHorizontalIntersectionsAndPadding(int i, Tuple<int, int> startPos, string word)
        {
            // Check letter before word
            if (i - 1 >= 0)
            {
                if (i == startPos.Item1)
                {
                    if (Board.Layout[startPos.Item2, i - 1] != _emptyChar)
                    {
                        return true;
                    }
                }
            }

            // Word Row
            if (Board.Layout[startPos.Item2, i] != _emptyChar)
            {

                // Check if same character at intersection, if not return true
                if (i < word.Length + startPos.Item1)
                {

                    if (word[i - startPos.Item1] != Board.Layout[startPos.Item2, i])
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            else // Padding
            {
                // Only allow padding check to be word length
                if (i < word.Length + startPos.Item1)
                {
                    // Check Padding Up
                    if (startPos.Item2 - 1 >= 0)
                    {

                        if (Board.Layout[startPos.Item2 - 1, i] != _emptyChar)
                        {
                            return true;
                        }
                    }

                    // Check Padding Down
                    if (startPos.Item2 + 1 < Height)
                    {

                        if (Board.Layout[startPos.Item2 + 1, i] != _emptyChar)
                        {

                            return true;
                        }
                    }
                }

            }
            return false;
        }

        // Checks Vertical Intersections
        private bool CheckVerticalIntersectionsAndPadding(int i, Tuple<int, int> startPos, string word)
        {

            // Check letter before word
            if (i - 1 >= 0)
            {
                if (i == startPos.Item2)
                {
                    if (Board.Layout[i - 1, startPos.Item1] != _emptyChar)
                    {
                        return true;
                    }
                }
            }
            // Word Column
            if (Board.Layout[i, startPos.Item1] != _emptyChar)
            {
                
                if (i < word.Length + startPos.Item2)
                {   // Check if same character at intersection, if not return true
                    if (word[i - startPos.Item2] != Board.Layout[i, startPos.Item1])
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
                
            }
            else // Padding
            {
                // Only allow padding check to be word length
                if (i < word.Length + startPos.Item2)
                {
                    // Check Padding Left
                    if (startPos.Item1 - 1 >= 0)
                    {

                        if (Board.Layout[i, startPos.Item1 - 1] != _emptyChar)
                        {
                            return true;
                        }
                    }

                    // Check Padding Right
                    if (startPos.Item1 + 1 < Width)
                    {

                        if (Board.Layout[i, startPos.Item1 + 1] != _emptyChar)
                        {
                            return true;
                        }
                    }

                }

            }
            return false;
        }


        // @ Zac to implement vacant space searching to assist with word placement
        #region VacantSpace

        public void CheckForVacantSpace()
        {
            var vacantRows = new List<VacantSpace>();

            var writingRow = false;
            var vacantRowCount = 0;

            Tuple<int, int> rowStart = null;

            // Horizontal Spaces
            for (var i = 0; i < Board.Layout.GetLength(0); i++)
            {
                for (var j = 0; j < Board.Layout.GetLength(1); j++)
                {

                    if (vacantRowCount >= 3)
                    {
                        writingRow = true;
                        if (rowStart == null)
                        {
                            rowStart = new Tuple<int, int>(j - 3, i);
                        }

                    }

                    // Finish row counting
                    if (j == Width - 1)
                    {

                        vacantRowCount = 0;

                        if (writingRow)
                        {
                            writingRow = false;

                            var rowEnd = new Tuple<int, int>(j, i);

                            vacantRows.Add(new VacantSpace() { VacantStart = rowStart, VacantEnd = rowEnd });

                            rowStart = null;

                        }

                    }

                    if (Board.Layout[i, j] == _emptyChar)
                    {
                        if (j == 0)
                        {
                            vacantRowCount = 0;
                        }
                        vacantRowCount++;

                    }
                    else // If cell not empty
                    {
                        vacantRowCount = 0;

                        if (writingRow)
                        {
                            writingRow = false;

                            var rowEnd = new Tuple<int, int>(j, i);

                            vacantRows.Add(new VacantSpace() { VacantStart = rowStart, VacantEnd = rowEnd });

                            rowStart = null;

                        }

                    }

                }

            }

            var vacantArray = new int[0];

            for (var i = 0; i < vacantRows.Count; i++)
            {


            }

            var arr1 = new int[] { 1, 2, 3, 4, 5};
            var arr2 = new int[] { 2, 3, 4};

            vacantArray = CompareIntArrays(arr1, arr2);

            for (var i = 0; i < vacantArray.Length; i++)
            {
                Debug.WriteLine(vacantArray[i]);
            }
        }


        // Compare two int array and return ...
        private int[] CompareIntArrays(int[] intArray1, int[] intArray2)
        {
            var returnArray = new List<int>();

            for (var i = 0; i < intArray1.Length; i++)
            {
                var num = intArray1[i];

                for (var j = 0; j < intArray2.Length; j++)
                {
                    if (num == intArray2[j])
                    {
                        returnArray.Add(num);
                    }
                }
            }

            return returnArray.ToArray();
        }


        // Print board to console
        public void DebugWriteLineBoard(BoardTemplate<char> board)
        {
            Debug.WriteLine("    0 1 2 3 4 5 6 7 8 9 1 2\n");
            for (var i = 0; i < board.Layout.GetLength(0); i++)
            {

                //var rowString = new string(_board[i]);

                var rowString = String.Empty;

                for (var j = 0; j < board.Layout.GetLength(1); j++)
                {
                    rowString += board.Layout[i, j] + " ";
                }

                var rowCount = "";

                if (i < 10)
                {
                    rowCount = "0" + i.ToString();
                }
                else
                {
                    rowCount = i.ToString();
                }
                Debug.WriteLine(rowCount + "   "+rowString);

            }

        }


    }

    // @ Zac to implement vacant space searching
    public class VacantSpace
    {
        public Tuple<int, int> VacantStart { get; set; }
        public Tuple<int, int> VacantEnd { get; set; }
    }
    #endregion
}


