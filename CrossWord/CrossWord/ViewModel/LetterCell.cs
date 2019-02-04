using CrossWord.Game;
using CrossWord.GameTemplates;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace CrossWord.ViewModel
{

    public class LetterCell : INotifyPropertyChanged
    {
        private char _letterOut;
        public char LetterOut
        {
            get { return _letterOut; }

            set
            {
                _letterOut = value;

                PropChangedHandler();
            }
        }

        private char _letterIn;
        public char LetterIn
        {
            get { return _letterIn; }
            set
            {
                _letterIn = value;
                
                PropChangedHandler();
            }
        }

        private string _definitionLocation;
        public string DefinitionLocation
        {
            get { return _definitionLocation; }

            set
            {
                _definitionLocation = value;

                PropChangedHandler();
            }
        }

        private Color _backgroundCellColour;
        public Color BackgroundCellColour
        {
            get { return _backgroundCellColour; }

            set
            {
                _backgroundCellColour = value;
                PropChangedHandler();
            }

        }

        private Color _foregroundColour;
        public Color FontColour
        {
            get { return _foregroundColour; }

            set
            {
                _foregroundColour = value;
                PropChangedHandler();
            }

        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                PropChangedHandler();
            }
        }

        private FontAttributes _fontWeight;
        public FontAttributes FontWeight
        {
            get { return _fontWeight; }
            set
            {
                _fontWeight = value;
                PropChangedHandler();
            }

        }

        private PlacedWordGroup _placedWordGroup;
        public PlacedWordGroup PlacedWordGroup
        {
            get { return _placedWordGroup;  }
            set
            {
                _placedWordGroup = value;
                PropChangedHandler();
            }

        }

        private Entry _cellEntry;
        public Entry CellEntry
        {
            get { return _cellEntry; }
            set { _cellEntry = value; }
        }

        private Tuple<int, int> _pos;
        public Tuple<int, int> Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void PropChangedHandler([CallerMemberName] String propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        
    }
}
