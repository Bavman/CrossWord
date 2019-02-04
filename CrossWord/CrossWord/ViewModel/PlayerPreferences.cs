using CrossWord.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CrossWord.ViewModel
{
    public class PlayerPreferences : INotifyPropertyChanged
    {
        #region Singleton Setup
        static PlayerPreferences _instance = null;

        private PlayerPreferences()
        {

        }

        public static PlayerPreferences Instance()
        {
            if (_instance == null)
            {
                _instance = new PlayerPreferences();
            }

            return _instance;
        }

        #endregion
        private int _themeIndex;

        public int ThemeIndex
        {
            get { return _themeIndex; }
            set
            {
                _themeIndex = value;
                PropChangedHandler();
            }
        }

        public int GridSize;
        public int WordCount;

        public event PropertyChangedEventHandler PropertyChanged;

        private void PropChangedHandler([CallerMemberName] String propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        // Load Player Preferences
        public void Load()
        {
            ThemeIndex = SharedClasses.Instance().AppClass.LoadPref<int>("ThemeIndex");
        }
    }


}
