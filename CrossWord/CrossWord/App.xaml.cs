using CrossWord.Game;
using CrossWord.View;
using CrossWord.ViewModel;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CrossWord
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            var gameController = new MainController();

            SharedClasses.Instance().AppClass = this;

            // Set up Colours
            PlayerPreferences.Instance().Load();
            Colours.Instance().CompileThemeList();
            Colours.Instance().SetThemeColours(PlayerPreferences.Instance().ThemeIndex);
            ChangeColours(PlayerPreferences.Instance().ThemeIndex);

            //MainPage = new CrosswordView();
            //MainPage = new SettingsView();
            MainPage = new NavigationPage (new HomeView());
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            PlayerScoreHistory.Instance().Load();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public void ChangeColours(int index)
        {
            var colours = Colours.Instance();

            colours.SetThemeColours(index);

            Resources["backgroundColour"] = colours.ColourThemes[index].Background;
            Resources["backgroundCellColour"] = colours.ColourThemes[index].BackgroundCell;
            Resources["backgroundGridColour"] = colours.ColourThemes[index].BackgroundGrid;
            Resources["buttonColour"] = colours.ColourThemes[index].BackgroundCell;
            Resources["foregroundColour"] = colours.ColourThemes[index].Forground;
            Resources["fontColour"] = colours.ColourThemes[index].Font;
            Resources["highlightColour"] = colours.ColourThemes[index].Highlight;
        }

        public void SavePref<T>(string id, object value)
        {
            Application.Current.Properties[id] = (T)value;
        }

        public T LoadPref<T>(string id)
        {
            if (Application.Current.Properties.ContainsKey(id))
            {
                return (T)Application.Current.Properties[id];
            }
            return default(T);
        }
    }
}
