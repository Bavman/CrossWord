using CrossWord.Game;
using CrossWord.GameTemplates;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace CrossWord.ViewModel
{
    public class Definition : INotifyPropertyChanged
    {
        private string _phrase;
        public string Phrase
        {
            get { return _phrase; }
            set
            {
                _phrase = value;
                PropChangedHandler();
            }
        }

        private string _index;
        public string Index
        {
            get { return _index; }
            set
            {
                _index = value;
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

        private Color _fontColour;
        public Color FontColour
        {
            get { return _fontColour; }

            set
            {
                _fontColour = value;
                PropChangedHandler();
            }

        }

        private bool _strikeThrough;
        public bool StrikeThrough
        {
            get { return _strikeThrough; }

            set
            {
                _strikeThrough = value;
                PropChangedHandler();
            }

        }

        public PlacedWord PlacedWord { get; set; }

        public WordDirection Direction { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void PropChangedHandler([CallerMemberName] String propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
