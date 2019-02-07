using CrossWord.Game;
using CrossWord.GameTemplates;
using CrossWord.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace CrossWord.View
{

    public partial class CrosswordView : ContentPage, INotifyPropertyChanged
    {

        private float _shortestScreenSideUnit = 22f;

        public static event StatusReceiverHandler ReceiveStatusEvent;

        private PlacedWord _currentPlacedWord;

        private WordChecker _wordChecker;

        private LetterCell _focusedLetterCell;

        private int _buttonCount;

        private bool _isPlacedWordSelected;

        private Score _score = new Score();

        private ListView _defList = new ListView();

        private bool _newGame;

        public CrosswordView()
        {
            InitializeComponent();

            ResetVariables();

            NavigationPage.SetHasNavigationBar(this, false);

            // Grid Crossword
            mainGrid.Children.Add(GenerateCrosswordGrid(CrosswordViewModel.Instance().DisplayBoard), 1, 1);

            // Definition List
            _defList = GenerateDefinitionList(CrosswordViewModel.Instance().Definitions);
            definitionGrid.Children.Add(_defList, 0, 1);

            ScoreLabel.BindingContext = _score;
            ScoreLabel.SetBinding(Label.TextProperty, new Binding("Value", BindingMode.OneWay, new StringToIntConverter()));
            SizeChanged += OnSizeChanged;
        }

        // InitializeVarialbes
        private void ResetVariables()
        {
            _currentPlacedWord = new PlacedWord();
            _wordChecker = new WordChecker();
            _focusedLetterCell = null;
            _score.Value = 0;
            _newGame = true;
        }

        // Update Layout on rotation of device
        private void OnSizeChanged(object sender, EventArgs e)
        {
            SetStyling();

            var smallestPercentage = CalcGridPercentage(_shortestScreenSideUnit);

            if (Width > Height)
            {
                // Horizontal
                Resources["orientationLabelStyle"] = Resources["horizontalLabelStyle"];
                Resources["orientationEntryStyle"] = Resources["horizontalEntryStyle"];

                mainGrid.RowDefinitions[0].Height = new GridLength(smallestPercentage);
                mainGrid.RowDefinitions[1].Height = new GridLength((_shortestScreenSideUnit - 3) * smallestPercentage);
                mainGrid.RowDefinitions[2].Height = new GridLength(2* smallestPercentage);

                mainGrid.ColumnDefinitions[0].Width = new GridLength(smallestPercentage);
                mainGrid.ColumnDefinitions[1].Width = new GridLength((_shortestScreenSideUnit - 2) * smallestPercentage);
                mainGrid.ColumnDefinitions[2].Width = new GridLength(2, GridUnitType.Auto);


                mainGrid.Children.Add(definitionGrid, 3, 1);
            }
            else
            {
                // Vertical
                Resources["orientationLabelStyle"] = Resources["verticalLabelStyle"];
                Resources["orientationEntryStyle"] = Resources["verticalEntryStyle"];

                //App.Current.Resources["labelSize"] = 50;
                mainGrid.RowDefinitions[0].Height = new GridLength(smallestPercentage);
                mainGrid.RowDefinitions[1].Height = new GridLength((_shortestScreenSideUnit - 4) * smallestPercentage);
                mainGrid.RowDefinitions[2].Height = new GridLength(2, GridUnitType.Auto);

                mainGrid.ColumnDefinitions[0].Width = new GridLength(2 * smallestPercentage);
                mainGrid.ColumnDefinitions[1].Width = new GridLength((_shortestScreenSideUnit - 4) * smallestPercentage);
                mainGrid.ColumnDefinitions[2].Width = new GridLength(2 * smallestPercentage);

                mainGrid.Children.Add(definitionGrid, 1, 3);
            }
        }

        // Calculate the 
        private float CalcGridPercentage(float divisionUnit)
        {

            if (Width > Height)
            {
                // Horizontal
                return 1 / divisionUnit * (float)Height;
            }
            else
            {
                // Vertical
                return 1 / divisionUnit * (float)Width;
            }
        }

        //Set Styling for Crossword UI Label and Enrty
        private void SetStyling()
        {

            var horizontalFontMult = 1 / _shortestScreenSideUnit * (float)Height;
            var verticalFontMult = 1 / _shortestScreenSideUnit * (float)Width;

            var fontGridSizeMult = 12/(float)MainController.GridSize;

            var horizontalLabelStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.FontSizeProperty, Value = horizontalFontMult * 0.5f * fontGridSizeMult}
                }
            };

            var verticalLabelStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.FontSizeProperty, Value = verticalFontMult * 0.5f * fontGridSizeMult}
                }

            };

            var horizontalEntryStyle = new Style(typeof(Entry))
            {
                Setters =
                {
                    new Setter {Property = Entry.FontSizeProperty, Value = horizontalFontMult * 0.7f * fontGridSizeMult}
                }
            };

            var verticalEntryStyle = new Style(typeof(Entry))
            {
                Setters =
                {
                    new Setter {Property = Entry.FontSizeProperty, Value = verticalFontMult * 0.7f * fontGridSizeMult}
                }

            };

            Resources = new ResourceDictionary
            {
                { "horizontalLabelStyle", horizontalLabelStyle },
                { "verticalLabelStyle", verticalLabelStyle },
                { "horizontalEntryStyle", horizontalEntryStyle },
                { "verticalEntryStyle", verticalEntryStyle }
            };

        }

        // Generate Definitions with template
        private ListView GenerateDefinitionList(ObservableCollection<Definition> definitions)
        {
            var defList = new ListView()
            {

            };
            defList.SetDynamicResource(ListView.BackgroundColorProperty, "foregroundColour");
            defList.ItemsSource = definitions;
            defList.ItemTemplate = new DataTemplate(typeof(DeffinitionCellView));

            defList.ItemSelected += (s, e) =>
            {
                HighlightSelectedWordFromDefinition((Definition)e.SelectedItem);
            };
            return defList;
        }

        // Generate Crossword Grid and Style
        private Grid GenerateCrosswordGrid(ObservableCollection<ObservableCollection<LetterCell>> board)
        {

            var crossWordGrid = new Grid()
            {
                ColumnSpacing = 1,
                RowSpacing = 1,
                Padding = 0
            };
            crossWordGrid.SetDynamicResource(Grid.BackgroundColorProperty, "backgroundGridColour");
            for (var i = 0; i < board.Count; i++)
            {
                crossWordGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            for (var i = 0; i < board[0].Count; i++)
            {
                crossWordGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            for (var i = 0; i < board.Count; i++)
            {

                for (var j = 0; j < board[i].Count; j++)
                {
                    var boxView = new BoxView()
                    {
                        CornerRadius = 2
                    };
                    boxView.BindingContext = board[i][j];
                    boxView.SetBinding(BoxView.BackgroundColorProperty, new Binding("BackgroundCellColour", BindingMode.OneWay));

                    var labelDefinitionLocation = new Label()
                    {
                        //Style = _resources["horizontalLabelStyle"] as Style
                        
                    };
                    labelDefinitionLocation.BindingContext = board[i][j];
                    labelDefinitionLocation.SetBinding(Label.TextProperty, new Binding("DefinitionLocation", BindingMode.OneWay));
                    labelDefinitionLocation.SetDynamicResource(Label.StyleProperty, "orientationLabelStyle");
                    labelDefinitionLocation.SetDynamicResource(Label.TextColorProperty, "fontColour");

                    var entry = new CustomEntry()
                    {
                        MaxLength = 1,
                        HorizontalTextAlignment = TextAlignment.Center,

                    };
                    entry.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeCharacter);

                    entry.BindingContext = board[i][j];
                    entry.SetBinding(CustomEntry.IsEnabledProperty, new Binding("IsEnabled", BindingMode.OneWay));
                    entry.SetBinding(CustomEntry.TextProperty, new Binding("LetterIn", BindingMode.TwoWay, new StringToCharConverter()));
                    entry.SetBinding(CustomEntry.TextColorProperty, new Binding("FontColour", BindingMode.OneWay));
                    entry.SetBinding(CustomEntry.FontAttributesProperty, new Binding("FontWeight", BindingMode.OneWay));
                    entry.SetDynamicResource(CustomEntry.StyleProperty, "orientationEntryStyle");
                    entry.TextChanged += LetterCell_TextChanged;

                    entry.Focused += (s, e) =>
                    {
                        EntryFocused((LetterCell)e.VisualElement.BindingContext);
                    };


                    board[i][j].CellEntry = entry;
                    board[i][j].Pos = new Tuple<int, int>(j, i);

                    var button = new Button()
                    {
                        BorderColor = Color.Transparent,
                        BackgroundColor = Color.Transparent
                    };
                    button.BindingContext = board[i][j];
                    button.SetBinding(Button.IsEnabledProperty, new Binding("IsEnabled", BindingMode.OneWay));
                    button.Pressed += (s, e) =>
                    {
                        var letterCell = (LetterCell)button.BindingContext;

                        if (_buttonCount < 1)
                        {
                            Device.StartTimer(TimeSpan.FromMilliseconds(500), ()=>DoubleClick(letterCell));
                        }
                        _buttonCount++;

                    };

                    crossWordGrid.Children.Add(boxView, j, i);
                    crossWordGrid.Children.Add(labelDefinitionLocation, j, i);
                    crossWordGrid.Children.Add(entry, j, i);
                    crossWordGrid.Children.Add(button, j, i);

                }

            }

            return crossWordGrid;
        }

        // Determines if single or double click
        bool DoubleClick(LetterCell letterCell)
        {
            if (_buttonCount > 1)
            {
                //Your action for Double Click here
                ButtonEntryFocused(letterCell, 2);
            }
            else
            {
                //Your action for Single Click here
                ButtonEntryFocused(letterCell, 1);
            }
            _buttonCount = 0;
            return false;
        }

        // Entry Letter Cell
        private void EntryFocused(LetterCell letterCell)
        {

        }

        // Actions single or double click methods
        private void ButtonEntryFocused(LetterCell letterCell, int clickCount)
        {
            _focusedLetterCell = letterCell;
            _currentPlacedWord.ColourizeSelectedWordCells(Colours.Forground);
            
            if (clickCount == 1)
            {
                if (letterCell.PlacedWordGroup.PlacedWord1 != null)
                {
                    _currentPlacedWord = letterCell.PlacedWordGroup.PlacedWord1;
                    letterCell.PlacedWordGroup.PlacedWord1.ColourizeSelectedWordCells(Colours.Highlight);
                    letterCell.CellEntry.Focus();
                    _isPlacedWordSelected = true;
                }
            }
            else
            {
                if (letterCell.PlacedWordGroup.PlacedWord2 != null)
                {
                    _currentPlacedWord = letterCell.PlacedWordGroup.PlacedWord2;
                    letterCell.PlacedWordGroup.PlacedWord2.ColourizeSelectedWordCells(Colours.Highlight);
                    letterCell.CellEntry.Focus();
                    _isPlacedWordSelected = true;
                }
                else
                {
                    _isPlacedWordSelected = false;
                }
            }
        }

        // Incriment cell focus to next available cell or defocus
        private void LetterCell_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Check if the character is not empty
            if (!IsCellEmpty(e))
            {
                return;
            }


            // Check if word is complete and correct - if so, defocus and dehighlight
            if (_wordChecker.IsWordCorrect())
            {
                // Update Score
                UpdateScoring();

                if (_focusedLetterCell != null)
                {
                    DeFocusCell();
                    _isPlacedWordSelected = false;
                }
            }
            else
            {
                // Exit if _focusedLetterCell is null
                if (!IsFocusedLetterCellNull())
                {
                    return;
                }

                // Exit if placed word is not selected
                if (!IsFocusedLetterCellNull())
                {
                    return;
                }

                // Change Entry focus to next cell in word
                // Return next cell position
                var nextCellPos = _currentPlacedWord.IncrimentCellPos(_focusedLetterCell.Pos);

                if (nextCellPos != null)
                {
                    // Clear character when selected
                    var letterCell = CrosswordViewModel.Instance().DisplayBoard[nextCellPos.Item2][nextCellPos.Item1];
                    if (letterCell.LetterIn != Char.MinValue)
                    {
                        letterCell.LetterIn = Char.MinValue;
                    }
                    _focusedLetterCell = letterCell;
                }
                else
                {
                    DeFocusCell();
                    _isPlacedWordSelected = false;
                }

                if (nextCellPos != null)
                {
                    CrosswordViewModel.Instance().DisplayBoard[nextCellPos.Item2][nextCellPos.Item1].CellEntry.Focus();
                }
            }
        }

        // Update Scoring
        private void UpdateScoring()
        {
            var value = 10;
            _score.Value += value;
            PlayerScoreHistory.Instance().AddTotalScore(value);
            PlayerScoreHistory.Instance().AddWordsSolved(1);

            if (_newGame)
            {
                PlayerScoreHistory.Instance().IncrementRoundsPlayed();
                _newGame = false;
            }
        }

        // Check if the character is not empty
        private bool IsCellEmpty(TextChangedEventArgs e)
        {
            if (e.NewTextValue != String.Empty)
            {
                return true;
            }
            return false;
        }

        // Check if _focusedLetterCell is not null
        private bool IsFocusedLetterCellNull()
        {
            if (_focusedLetterCell != null)
            {
                return true;
            }
            return false;
        }
        
        // Check if placed word is selected
        private bool IsPlacedWordSelected()
        {
            if (_isPlacedWordSelected)
            {
                return true;
            }
            return false;
        }

        // Defocus cell
        private void DeFocusCell()
        {
            _focusedLetterCell.CellEntry.Unfocus();

            _currentPlacedWord.ColourizeSelectedWordCells(Colours.Forground);
        }

        // Highlights word from definition selection
        private void HighlightSelectedWordFromDefinition(Definition definition)
        {
            _currentPlacedWord.ColourizeSelectedWordCells(Colours.Forground);

            definition.PlacedWord.ColourizeSelectedWordCells(Colours.Highlight);
            
            _currentPlacedWord = definition.PlacedWord;
        }

        // Solve Board
        private void SolveBoard(ObservableCollection<ObservableCollection<LetterCell>> board)
        {
            for (var i = 0; i < board.Count; i++)
            {
                for (var j = 0; j < board[i].Count; j++)
                {
                    board[i][j].LetterIn = board[i][j].LetterOut;
                }
            }
        }

        #region Buttons

        private void ButtonPrintList_Clicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Print Board");
            CrosswordViewModel.Instance().PrintBoard();
        }

        private void Home_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void ButtonSolve_Clicked(object sender, EventArgs e)
        {
            SolveBoard(CrosswordViewModel.Instance().DisplayBoard);
        }

        private void ButtonRegenBoard_Clicked(object sender, EventArgs e)
        {
            ResetVariables();
            _wordChecker.FoundWords.Clear();
            CrosswordBoardMain.Instance().ResetBoard();
            CrosswordBoardPlacedWord.Instance().ResetBoard();

            MainController.SetupCrosswordPuzzle(CrosswordViewModel.Instance(), CrosswordBoardMain.Instance(), PlacementManager.Instance());
        }

        #endregion

        private void OnReceiveStatus()
        {
            if (ReceiveStatusEvent != null)
            {
                ReceiveStatusEvent(this, new StatusReceiverEventArgs());
            }
        }

        private void PrintPlacedWord_Clicked(object sender, EventArgs e)
        {
            CrosswordViewModel.Instance().PrintPlacedWordBoard();
        }
    }
}
