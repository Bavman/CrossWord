using CrossWord.GameTemplates;
using CrossWord.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace CrossWord.Game
{
    public class DefinitionSortingTools
    {

        // Sort placed words into horizontal order and assign definition and index to placedWord
        public List<PlacedWord> SortPlacedWordsByPosition(List<PlacedWord> placedWords)
        {
            var placedWordsSorted = new List<PlacedWord>();

            // Sort in ascending
            placedWordsSorted = placedWords.OrderBy(w => w.StartPos.Item2).ThenBy(w => w.StartPos.Item1).ToList();

            return FindDefinitionsAndAssignLocationIndex(placedWordsSorted);
        }

        // After words are placed and sorted, this method looks up the placed word definition
        public List<PlacedWord> FindDefinitionsAndAssignLocationIndex(List<PlacedWord> placedWords)
        {
            var placedWordWithDeets = new List<PlacedWord>();

            var previousStartPos = new Tuple<int, int>(-1, -1);

            var count = 0;

            for (var i = 0; i < placedWords.Count; i++)
            {
                var definitionArray = WordList.Instance().WordAndDefinitions.Where(d => d.Word == placedWords[i].Word).ToArray();

                if (definitionArray == null)
                {
                    return null;
                }
                var definition = definitionArray[0].Definition;

                if (!previousStartPos.Equals(placedWords[i].StartPos))
                {
                    count++;
                }

                placedWordWithDeets.Add(new PlacedWord
                {
                    Word = placedWords[i].Word,
                    StartPos = placedWords[i].StartPos,
                    Direction = placedWords[i].Direction,
                    DefinitionIndex = count.ToString(),
                    Definition = definition,
                });

                // Assign Definition index to placedWord
                placedWords[i].DefinitionIndex = count.ToString();

                previousStartPos = placedWords[i].StartPos;
            }

            return placedWordWithDeets;
        }

        // Sort and find definition of placed words
        public IEnumerable<PlacedWord> GroupPlacedWords(List<PlacedWord> placedWords)
        {
            var sortedHorizontalPlacedWords = SortPlacedWordsByAxis(placedWords, WordDirection.Horizontal);
            var sortedVerticalPlacedWords = SortPlacedWordsByAxis(placedWords, WordDirection.Vertical);

            sortedHorizontalPlacedWords.Insert(0, (new PlacedWord { Definition = "ACROSS" }));
            sortedVerticalPlacedWords.Insert(0, (new PlacedWord { Definition = "DOWN" }));

            var joinedLists = sortedHorizontalPlacedWords.Concat(sortedVerticalPlacedWords);

            return joinedLists;

        }

        // Sort placed words based on start postions.
        public List<PlacedWord> SortPlacedWordsByAxis(List<PlacedWord> placedWords, WordDirection direction)
        {

            var placedWordsSorted = new List<PlacedWord>();
            // Sorted Horizontal or vertical
            if (direction == WordDirection.Horizontal)
            {
                placedWordsSorted = placedWords.Where(w => w.Direction == direction)
                    .OrderBy(w => Convert.ToInt32(w.DefinitionIndex)).ToList();
            }
            if (direction == WordDirection.Vertical)
            {
                placedWordsSorted = placedWords.Where(w => w.Direction == direction)
                    .OrderBy(w => Convert.ToInt32(w.DefinitionIndex)).ToList();
            }


            return placedWordsSorted;
        }

        // Asign definition from placedWords to definition class
        public IEnumerable<Definition> ReturnDefinitionArray(IEnumerable<PlacedWord> placedWords)
        {
            var definitions = placedWords.Select(d => new Definition
            {
                Phrase = d.Definition,
                Index = d.DefinitionIndex,
                FontColour = Colours.Font,
                Direction = d.Direction,
                PlacedWord = d
            });

            return definitions;
        }

        // Apply bold on definitions that are "ACROSS" and "BOLD"
        public IEnumerable<Definition> ApplyDefinitionHeadingFormatting(IEnumerable<Definition> definitions)
        {

            var def = definitions.ToArray();

            for (var i = 0; i < def.Length; i++)
            {
                if (def[i].Phrase == "ACROSS" || def[i].Phrase == "DOWN")
                {
                    def[i].FontWeight = FontAttributes.Bold;
                }
                else
                {
                    def[i].FontWeight = FontAttributes.None;
                }
            }

            return def;
        }
    }
}