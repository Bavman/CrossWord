using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CrossWord.ViewModel
{
    public class Colours
    {
        #region Singleton Setup
        static Colours _instance = null;

        private Colours()
        {

        }

        public static Colours Instance()
        {
            if (_instance == null)
            {
                _instance = new Colours();
            }

            return _instance;
        }
        #endregion

        public static Color Background;
        public static Color BackgroundCell;
        public static Color BackgroundGrid;
        public static Color Forground;
        public static Color Font;
        public static Color Highlight;
        public static Color FoundWord;

        public List<ColourTheme> ColourThemes;

        // Complie Themes into a ColourTheme list and return
        public List<ColourTheme> CompileThemeList()
        {
            ColourThemes = new List<ColourTheme>
            {
                Theme0,
                Theme1,
                Theme2
            };

            return ColourThemes;
        }

        // 1 Light Theme
        public ColourTheme Theme0 = new ColourTheme()
        {
            Index = 0,
            Background = Color.FromHex("#FFFFFF"),
            BackgroundCell = Color.FromHex("#cce6ff"),
            BackgroundGrid = Color.FromHex("#80c1ff"),
            Forground = Color.FromHex("#f2f2f2"),
            Font = Color.FromHex("#262626"),
            Highlight = Color.FromHex("#ffff00"),
            FoundWord = Color.FromHex("#66b3ff")
        };

        // 2 Dark Theme
        public ColourTheme Theme1 = new ColourTheme()
        {
            Index = 1,
            Background = Color.FromHex("#000000"),
            BackgroundCell = Color.FromHex("#212121"),
            BackgroundGrid = Color.FromHex("#1a1a1a"),
            Forground = Color.FromHex("#808080"),
            Font = Color.FromHex("#e6e6e6"),
            Highlight = Color.FromHex("#ff6699"),
            FoundWord = Color.FromHex("#66ff66")
        };

        // 3 Alt Theme 1
        public ColourTheme Theme2 = new ColourTheme()
        {
            Index = 2,
            Background = Color.FromHex("#8000ff"),
            BackgroundCell = Color.FromHex("#79d279"),
            BackgroundGrid = Color.FromHex("#53c653"),
            Forground = Color.FromHex("#f2f2f2"),
            Font = Color.FromHex("#262626"),
            Highlight = Color.FromHex("#ffff00"),
            FoundWord = Color.FromHex("#66cc66"),
        };

        // Set the Theme Colours
        public void SetThemeColours(int index)
        {
            var colourTheme = ColourThemes[index];

            Background = colourTheme.Background;
            BackgroundCell = colourTheme.BackgroundCell;
            BackgroundGrid = colourTheme.BackgroundGrid;
            Forground = colourTheme.Forground;
            Font = colourTheme.Font;
            Highlight = colourTheme.Highlight;
            FoundWord = colourTheme.FoundWord;
        }

    }
}
