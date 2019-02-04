using CrossWord.Game;
using CrossWord.GameTemplates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace CrossWord.ViewModel
{
    public delegate void StatusReceiverHandler(object sender, StatusReceiverEventArgs e);

    public class StatusReceiverEventArgs : EventArgs
    {
        public StatusReceiverEventArgs()
        {

        }

    }

    public class CrosswordViewModel
    {

        #region Singleton Setup
        static CrosswordViewModel _instance = null;

        private CrosswordViewModel()
        {

        }

        public static CrosswordViewModel Instance()
        {
            if (_instance == null)
            {
                _instance = new CrosswordViewModel();

            }

            return _instance;
        }

        #endregion

        private ObservableCollection<ObservableCollection<LetterCell>> _displayBoard;
        public ObservableCollection<ObservableCollection<LetterCell>> DisplayBoard
        {
            get
            {
                return _displayBoard;
            }
            set
            {
                _displayBoard = value;
            }
        }

        private ObservableCollection<Definition> _definitions = new ObservableCollection<Definition>();
        public ObservableCollection<Definition> Definitions
        {
            get { return _definitions; }
            set { _definitions = value; }
        }

        // Initializes definition list
        public void AssignDefinitionList (IEnumerable<Definition> definitions)
        {

            Definitions.Clear();

            var definitionArray = definitions.ToArray();

            for (var i = 0; i < definitionArray.Length; i++)
            {
                Definitions.Add(definitionArray[i]);
            }

        }

        // Initializes display board
        public void InitializeDisplayBoard (int width, int height)
        {
            var board = new ObservableCollection<ObservableCollection<LetterCell>>();

            for (var i = 0; i < height; i++)
            {

                var newRow = new ObservableCollection<LetterCell>();

                for (var j = 0; j < width; j++)
                {
                    var cell = new LetterCell { };

                    newRow.Add(cell);
                }

                board.Add(newRow);
            }

            DisplayBoard = board;
        }

        // Assigs placed words to LetterCell multidimensional array and sets formatting
        public void AssignCrosswordDisplayBoard (BoardTemplate<char> board, BoardTemplate<PlacedWordGroup> placedWordBoard)
        {

            for (var i = 0; i < board.Height; i++)
            {

                for (var j = 0; j < board.Width; j++)
                {
                    DisplayBoard[i][j].LetterOut = board.Layout[i, j];
                    DisplayBoard[i][j].LetterIn = '\0';
                    DisplayBoard[i][j].DefinitionLocation = String.Empty;
                    DisplayBoard[i][j].FontWeight = FontAttributes.None;
                    DisplayBoard[i][j].FontColour = Colours.Font;

                    DisplayBoard[i][j].PlacedWordGroup = placedWordBoard.Layout[i, j];

                    if (board.Layout[i, j] == board.EmptyChar)
                    {
                        DisplayBoard[i][j].BackgroundCellColour = Colours.BackgroundCell;
                        DisplayBoard[i][j].IsEnabled = false;
                    }
                    else
                    {
                        DisplayBoard[i][j].BackgroundCellColour = Colours.Forground;
                        DisplayBoard[i][j].IsEnabled = true;
                    }

                }

            }

        }

        // Print Display Board to console
        public void PrintBoard()
        {
            for (var i = 0; i < DisplayBoard.Count; i++)
            {
                var row = String.Empty;

                for (var j = 0; j < DisplayBoard[i].Count; j++)
                {
                    row += DisplayBoard[i][j].LetterIn + " ";
                }
                Debug.WriteLine(row);
            }
        }

        // Print Display Board to console
        public void PrintPlacedWordBoard()
        {
            for (var i = 0; i < DisplayBoard.Count; i++)
            {
                var row = String.Empty;

                for (var j = 0; j < DisplayBoard[i].Count; j++)
                {
                    if (DisplayBoard[i][j].PlacedWordGroup.PlacedWord1.Word != null)
                    {
                        row += DisplayBoard[i][j].PlacedWordGroup.PlacedWord1.Word[0] + " ";
                    }
                    else
                    {
                        row += "  ";
                    }
                    
                }
                Debug.WriteLine(row);
            }
        }

        // Print list to console
        public void PrintList(ObservableCollection<LetterCell> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                Debug.WriteLine(list[i].LetterOut);
            }
        }

    }


}
