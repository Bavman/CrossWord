using CrossWord.Core;
using CrossWord.GameTemplates;
using CrossWord.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace CrossWord.Game
{
    public class PlacementManager
    {
        #region Singleton Setup
        static PlacementManager _instance = null;

        private PlacementManager()
        {

        }

        public static PlacementManager Instance()
        {
            if (_instance == null)
            {
                _instance = new PlacementManager();
            }

            return _instance;
        }

        #endregion

        public IEnumerable<PlacedWord> GroupedWords;

        public List<PlacedWord> PlacedWords = new List<PlacedWord>();

        public IEnumerable<Definition> Definitions;

        private Random _random = new Random();

        private int _count;

        private int[] _wordSizes = 
        {
            9, 8, 8, 7, 7, 7, 7, 6, 6, 6, 5, 5, 5, 4, 4, 4, 6, 6, 5, 5, 5, 5, 5, 6, 6,
            5, 5, 5, 4, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3
        };

        private List<string> _usedWords = new List<string>();

        private int _wordsPlaced;

        DefinitionSortingTools _definitionSortingTools = new DefinitionSortingTools();

        // Keeps trying to solve board until the _minWordsPlaced count is reached 
        public void PlaceAllWords()
        {
            PlacedWords.Clear();
            _usedWords.Clear();
            _wordsPlaced = 0;
            _count = 0;

            var solved = false;

            while (solved == false)
            {
                PlaceWords();

                if (_wordsPlaced >= MainController.MinWordsPlaced)
                {
                    solved = true;
                    break;
                }

                // Reset the board and start again if min word count in not met
                if (!solved)
                {
                    CrosswordBoardMain.Instance().ResetBoard();

                    _wordsPlaced = 0;
                    _usedWords = new List<string>();
                    solved = false;
                }
            }

            // Assign placed words location to each cell with a character (used for highlighting words)
            CrosswordBoardPlacedWord.Instance().AssignPlacedWords(PlacedWords);

            // Sort and find definition of placed words then apply formatting
            var SortedPlacedWords = _definitionSortingTools.SortPlacedWordsByPosition(PlacedWords);

            GroupedWords = _definitionSortingTools.GroupPlacedWords(SortedPlacedWords);

            var definitions = _definitionSortingTools.ReturnDefinitionArray(GroupedWords);

            Definitions = _definitionSortingTools.ApplyDefinitionHeadingFormatting(definitions);

        }

        // Try placing all words on board itterating through _wordSizes array for word sizes
        private void PlaceWords()
        {
            _count++;
            PlacedWords.Clear();

            // First Word
            PlaceFirstWord();

            // Rest of board
            var direction = WordDirection.Vertical;

            // Sequence through word size array
            for (var i = 1; i < _wordSizes.Length; i++)
            {

                var wordAndPosList = FindWordAndPositions(_wordSizes[i], direction);

                if (wordAndPosList != null)
                {

                    var startPositions = new List<Tuple<int, int>>(wordAndPosList.StartPosList);

                    var word = wordAndPosList.Word;

                    var usedCharsIndex = new List<int> { };

                    if (startPositions.Count > 0)
                    {

                        var whileCount = 0;

                        // Try start positions until word placement is found
                        while (whileCount < startPositions.Count)
                        {
                            // Randomly itterate through start positions
                            var listIndex = ReturnRandomNumberExcludingArrayInts(startPositions.Count, usedCharsIndex);
                            usedCharsIndex.Add(listIndex);

                            var startPos = startPositions[listIndex];

                            var placedWord = CrosswordBoardMain.Instance().PlaceWord(word, startPos, direction);

                            if (placedWord != null)
                            {

                                _usedWords.Add(word);

                                if (direction == WordDirection.Horizontal)
                                {
                                    direction = WordDirection.Vertical;
                                }
                                else
                                {
                                    direction = WordDirection.Horizontal;
                                }

                                PlacedWords.Add(placedWord);

                                _wordsPlaced++;

                                // Break from while if wordcount reached
                                if (_wordsPlaced >= MainController.MinWordsPlaced)
                                {
                                    break;
                                }

                                break;
                            }

                            whileCount++;
                        }

                    }

                }

            }

        }

        // Place first word on board
        private void PlaceFirstWord()
        {
            var word = RetrieveWord(new List<string> { }, _wordSizes[0]);

            var random = new Random();
            var horizontalPos = random.Next(0, 1);
            var vertiacalPos = random.Next(4, 6);
            var randomStartPos = new Tuple<int, int>(horizontalPos, vertiacalPos);

            // Place first word
            var placedWord = CrosswordBoardMain.Instance().PlaceWord(word, randomStartPos, WordDirection.Horizontal);
            if (placedWord != null)
            {
                PlacedWords.Add(placedWord);
            }
            else
            {
                Debug.WriteLine("IsNull");
            }
            _usedWords.Add(word);
        }

        // Get new word from WordList Words based no letter count
        public string RetrieveWord(List<string> usedWords, int letterCount)
        {

            var wordList = WordList.Instance().WordAndDefinitions.Select(w => w.Word).ToArray();

            var wordArray = wordList.Where(w => w.Length == letterCount).
                Except(usedWords).
                ToArray();

            var word = string.Empty;

            if (wordArray == null)
            {
                var opResults = new OperationResult() { Success = false };
                opResults.AddMessage("Ran out of words");

                return null;
            }

            if (wordArray.Length == 0)
            {
                var opResults = new OperationResult() { Success = false };
                opResults.AddMessage("No words with the desired lettercount");

                return null;
            }
            
            return wordArray[_random.Next(wordArray.Length)]; ;

        }

        // Itterates through board looking for potential letters where words can intersect
        private WordAndStartPositions FindWordAndPositions(int wordLength, WordDirection direction)
        {
            var startPosAttempts = 0;
            var word = String.Empty;
            var usedWords = new List<string>(_usedWords);
            var starPosList = new List<Tuple<int, int>> {};
            var maxCounts = 25;

            // Attempt to find start positions
            while (startPosAttempts < maxCounts)
            {
                word = RetrieveWord(usedWords, wordLength);

                var wordCharCount = 0;

                // Attemps cycling through letters
                while (wordCharCount < word.Length)
                {

                    var posList = CrosswordBoardMain.Instance().GetWordStartPositions(word, wordCharCount, direction);
                    starPosList.AddRange(posList);

                    wordCharCount ++;
                }

                if (starPosList.Count > 0)
                {

                    break;
                }

                usedWords.Add(word);
               
                startPosAttempts ++;
            }

            // Return null of no position found
            if (starPosList.Count == 0)
            {
                Debug.WriteLine("____________failed to find startPos");
                return null;
            }

            var wordAndPosList = new WordAndStartPositions
            {
                StartPosList = new List<Tuple<int, int>>(starPosList),
                Word = word
            };

            return wordAndPosList;
        }

        // Returns a random number from a int List considering the used numbers
        public int ReturnRandomNumberExcludingArrayInts(int length, List<int> usedNumbers)
        {
            var sequenceArray = Enumerable.Range(0, length).ToArray();

            var availableInts = sequenceArray.Except(usedNumbers).ToArray();


            var random = new Random();

            var result = availableInts[random.Next(0, availableInts.Length)];
            
            return result;
        }

        // Generate board that holds the across and down definition numbers
        public void ApplyDefinitionNumbersToDisplayBoard(IEnumerable<PlacedWord> placedWords, ObservableCollection<ObservableCollection<LetterCell>> displayBoard)
        {
            foreach (var placedWord in placedWords)
            {

                if (placedWord.DefinitionIndex != null)
                {
                    displayBoard[placedWord.StartPos.Item2][placedWord.StartPos.Item1].DefinitionLocation = placedWord.DefinitionIndex;
                }

            }

        }
    }

}
