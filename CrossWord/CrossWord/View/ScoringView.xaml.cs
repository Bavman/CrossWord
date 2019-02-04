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
    public partial class ScoringView : ContentPage
    {
        public ScoringView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            GamesPlayed.BindingContext = PlayerScoreHistory.Instance();
            GamesPlayed.SetBinding(Label.TextProperty, new Binding("RoundsPlayed", BindingMode.OneWay, new StringToIntConverter()));

            WordsSolved.BindingContext = PlayerScoreHistory.Instance();
            WordsSolved.SetBinding(Label.TextProperty, new Binding("WordsSolved", BindingMode.OneWay, new StringToIntConverter()));

            TotalScore.BindingContext = PlayerScoreHistory.Instance();
            TotalScore.SetBinding(Label.TextProperty, new Binding("TotalScore", BindingMode.OneWay, new StringToIntConverter()));
        }

        private void Home_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}