using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CrossWord.ViewModel
{
    public class PlayerScoreHistory : INotifyPropertyChanged
    {
        #region Singleton Setup
        static PlayerScoreHistory _instance = null;

        private PlayerScoreHistory()
        {

        }

        public static PlayerScoreHistory Instance()
        {
            if (_instance == null)
            {
                _instance = new PlayerScoreHistory();
            }

            return _instance;
        }

        #endregion

        private int _roundsPlayed;
        public int RoundsPlayed
        {
            get { return _roundsPlayed; }
            set
            {
                _roundsPlayed = value;
                PropChangedHandler();
            }
        }

        private int _wordsSolved;
        public int WordsSolved
        {
            get { return _wordsSolved; }
            set
            {
                _wordsSolved = value;
                PropChangedHandler();
            }
        }


        private int _totalScore;
        public int TotalScore
        {
            get { return _totalScore; }
            set
            {
                _totalScore = value;
                PropChangedHandler();
            }
        }


        // Increment and save rounds played
        public void IncrementRoundsPlayed()
        {
            RoundsPlayed++;
            SharedClasses.Instance().AppClass.SavePref<int>("RoundsPlayed", RoundsPlayed);
            Debug.WriteLine("RoundsPlayed " + RoundsPlayed);
        }

        // Increment and save words solved
        public void AddWordsSolved(int value)
        {
            WordsSolved += value;
            SharedClasses.Instance().AppClass.SavePref<int>("WordsSolved", WordsSolved);
            Debug.WriteLine("WordsSolved " + WordsSolved);
        }

        // Increment and save total score
        public void AddTotalScore(int value)
        {
            TotalScore += value;
            SharedClasses.Instance().AppClass.SavePref<int>("TotalScore", TotalScore);
            Debug.WriteLine("TotalScore " + TotalScore);
        }

        // Load rounds played, words solved and score
        public void Load()
        {
            RoundsPlayed = SharedClasses.Instance().AppClass.LoadPref<int>("RoundsPlayed");
            WordsSolved = SharedClasses.Instance().AppClass.LoadPref<int>("WordsSolved");
            TotalScore = SharedClasses.Instance().AppClass.LoadPref<int>("TotalScore");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void PropChangedHandler([CallerMemberName] String propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
