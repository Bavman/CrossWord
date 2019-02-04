using CrossWord.Game;
using CrossWord.ViewModel;
using System;
using System.Diagnostics;
using System.Drawing;
using Xamarin.Forms;

namespace CrossWord.GameTemplates
{
    public class PlacedWord
    {
        public Tuple<int, int> StartPos;
        public string Word;
        public WordDirection Direction;
        public string DefinitionIndex;
        public string Definition;
        public bool Focused;

        // Colours word cells 
        public void ColourizeSelectedWordCells(Xamarin.Forms.Color color)
        {
            if (Word != null)
            {
                for (var i = 0; i < Word.Length; i++)
                {

                    if (Direction == WordDirection.Horizontal)
                    {
                        CrosswordViewModel.Instance().DisplayBoard[StartPos.Item2][StartPos.Item1 + i].BackgroundCellColour = color;
                    }
                    else
                    {
                        CrosswordViewModel.Instance().DisplayBoard[StartPos.Item2 + i][StartPos.Item1].BackgroundCellColour = color;
                    }
                }
            }
        }

        // Increments to next entry focus when typing in word letters 
        public Tuple<int, int> IncrimentCellPos(Tuple<int, int> previousPos)
        {
            Tuple<int, int> returnPos = null;

            for (var i = 0; i < Word.Length; i++)
            {
                if (Direction == WordDirection.Horizontal)
                {

                    if (previousPos.Item1 == StartPos.Item1 + i - 1)
                    {                      
                        returnPos = new Tuple<int, int>(previousPos.Item1 + 1, StartPos.Item2);
                        return returnPos;
                    }

                }
                else
                {
                    if (previousPos.Item2 == StartPos.Item2 + i - 1)
                    {
                        returnPos = new Tuple<int, int>(StartPos.Item1, previousPos.Item2 + 1);
                        return returnPos;
                    }


                }
            }
            return returnPos;
        }

    }
}
