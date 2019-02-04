using CrossWord.Game;
using CrossWord.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossWord.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomeView : ContentPage
	{

        CrosswordView _crosswordView;
        SettingsView _settingsView;
        ScoringView _scoringView;

        public HomeView ()
		{
			InitializeComponent ();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        // Go to crossword game page
        private void Crossword_Clicked(object sender, EventArgs e)
        {
            MainController.ResetDisplayBoard();

            if (_crosswordView == null)
            {
                _crosswordView = new CrosswordView();
            }

            Navigation.PushAsync(_crosswordView);
        }

        // Go to settings page
        private void Settings_Clicked(object sender, EventArgs e)
        {
            if (_settingsView == null)
            {
                _settingsView = new SettingsView();
            }
            Navigation.PushAsync(_settingsView);
        }

        // Go to scoring page
        private void Scoring_Clicked(object sender, EventArgs e)
        {

            if(_scoringView == null)
            {
                _scoringView = new ScoringView();
            }
            Navigation.PushAsync(_scoringView);
        }
    }
}