using CrossWord.GameTemplates;
using CrossWord.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CrossWord.Game
{
    public class CrosswordBoardPlacedWord
    {

        #region Singleton Setup
        static CrosswordBoardPlacedWord _instance = null;

        private CrosswordBoardPlacedWord()
        {

        }

        public static CrosswordBoardPlacedWord Instance()
        {
            if (_instance == null)
            {
                _instance = new CrosswordBoardPlacedWord();
            }

            return _instance;
        }

        #endregion

        public int Width;
        public int Height;

        public BoardTemplate<PlacedWordGroup> Board;

        public void InitializeBoard(int width, int height)
        {
            Board = new BoardTemplate<PlacedWordGroup>(height, width, new PlacedWordGroup());
            Width = width;
            Height = height;
        }

        // Generate PlacedWords Board that is used to identify and highlight selected words
        public void AssignPlacedWords(List<PlacedWord> placedWordsList)
        {

            for (var i = 0; i < placedWordsList.Count; i++)
            {

                for (var j = 0; j < placedWordsList[i].Word.Length; j++)
                {
                    var startPos = placedWordsList[i].StartPos;


                    if (placedWordsList[i].Direction == WordDirection.Horizontal)
                    {
                        if (Board.Layout[startPos.Item2, startPos.Item1 + j].PlacedWord1 == null)
                        {
                            Board.Layout[startPos.Item2, startPos.Item1 + j].PlacedWord1 = placedWordsList[i];
                        }
                        else
                        {
                            Board.Layout[startPos.Item2, startPos.Item1 + j].PlacedWord2 = placedWordsList[i];
                        }
                       
                    }
                    else
                    {
                        if (Board.Layout[startPos.Item2 + j, startPos.Item1].PlacedWord1 == null)
                        {
                            Board.Layout[startPos.Item2 + j, startPos.Item1].PlacedWord1 = placedWordsList[i];
                        }
                        else
                        {
                            Board.Layout[startPos.Item2 + j, startPos.Item1].PlacedWord2 = placedWordsList[i];
                        }

                    }

                }

            }

        }

        // Reset Board
        public void ResetBoard()
        {

            for (var i = 0; i < Height; i++)
            {

                for (var j = 0; j < Width; j++)
                {
                    Board.Layout[i, j] = new PlacedWordGroup();

                }

            }

        }

    }
}
