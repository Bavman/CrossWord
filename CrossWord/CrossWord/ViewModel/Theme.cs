using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace CrossWord.ViewModel
{
    public class Theme : INotifyPropertyChanged
    {

        private Color _backgroundCellColour;
        public Color BackgroundCellColour
        {
            get
            {
                return _backgroundCellColour;
            }
            set
            {
                _backgroundCellColour = value;
                PropChangedHandler();
            }
        }

        private Color _fontColour;
        public Color FontColour
        {
            get
            {
                return _fontColour;
            }
            set
            {
                _fontColour = value;
                PropChangedHandler();
            }
        }

        public string ThemeName { get; set; }
        public int ThemeIndex { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void PropChangedHandler([CallerMemberName] String propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
