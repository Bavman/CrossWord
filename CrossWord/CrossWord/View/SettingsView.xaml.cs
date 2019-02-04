using CrossWord.Game;
using CrossWord.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossWord.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsView : ContentPage
	{
        public static App AppClass;

        private Theme _prevoiusTheme;

        private Colours _colours = Colours.Instance();

        private ListView _themeList;

        private List<string> _crosswordSizeList = new List<string>
        {
            "Small",
            "Medium",
            "Large",
            "Extra Large"
        };

        public SettingsView ()
		{
            AppClass = SharedClasses.Instance().AppClass;

            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            ThemeGrid.Children.Add(GenerateThemeList(ReturnThemeColours()), 1, 0);

            _themeList.ScrollTo(ReturnThemeColours()[PlayerPreferences.Instance().ThemeIndex], ScrollToPosition.Start, false);
            
        }


        // Initialize Theme Colours
        private List<Theme> ReturnThemeColours()
        {
            return new List<Theme>()
            {
                new Theme(){ThemeIndex = 0, BackgroundCellColour = _colours.Theme0.BackgroundCell, FontColour = _colours.Theme0.Font, ThemeName="Light"},
                new Theme(){ThemeIndex = 1, BackgroundCellColour = _colours.Theme1.BackgroundCell, FontColour = _colours.Theme1.Font, ThemeName="Dark"},
                new Theme(){ThemeIndex = 2, BackgroundCellColour = _colours.Theme2.BackgroundCell, FontColour = _colours.Theme2.Font, ThemeName="Theme 1"},
            };
        }


        // Generate Definitions with template
        private ListView GenerateThemeList(List<Theme> themes)
        {
            _themeList = new ListView()
            {
                
            };

            _themeList.ItemsSource = themes;
            _themeList.ItemTemplate = new DataTemplate(typeof(ThemeCellView));
            _themeList.SeparatorVisibility = 0;
            _themeList.ItemSelected += (s, e) =>
            {
                SelectTheme((Theme)e.SelectedItem);
            };

            return _themeList;
        }

        // Select Theme
        private void SelectTheme(Theme theme)
        {

            SharedClasses.Instance().AppClass.ChangeColours(theme.ThemeIndex);
            SharedClasses.Instance().AppClass.SavePref<int>("ThemeIndex", theme.ThemeIndex);

            _prevoiusTheme = theme;
        }

        // Go to home page
        private void Home_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}