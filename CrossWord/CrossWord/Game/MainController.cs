using CrossWord.GameTemplates;
using CrossWord.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossWord.Game
{
    public class MainController
    {

        public static int GridSize;
        public static int MinWordsPlaced;

        public MainController()
        {
            // Grid size 10, MinWordsPlaced = 12
            // Grid size 12, MinWordsPlaced = 15
            // Grid size 14, MinWordsPlaced = 21
            // Grid size 16, MinWordsPlaced = 24

            GridSize = 12;
            MinWordsPlaced = 15;


            CrosswordBoardMain.Instance().InitializeBoard(GridSize, GridSize, Char.MinValue);
            CrosswordBoardPlacedWord.Instance().InitializeBoard(GridSize, GridSize);
            CrosswordViewModel.Instance().InitializeDisplayBoard(GridSize, GridSize);

        }

        public static void ResetDisplayBoard()
        {
            CrosswordBoardMain.Instance().ResetBoard();
            CrosswordBoardPlacedWord.Instance().ResetBoard();
            CrosswordViewModel.Instance().AssignCrosswordDisplayBoard(CrosswordBoardMain.Instance().Board, CrosswordBoardPlacedWord.Instance().Board);
            CrosswordViewModel.Instance().Definitions.Clear();
        }

        public static void SetupCrosswordPuzzle(CrosswordViewModel crosswordViewModel, CrosswordBoardMain crosswordBoardMain, PlacementManager placementManager)
        {
            placementManager.PlaceAllWords();

            crosswordBoardMain.DebugWriteLineBoard(crosswordBoardMain.Board);

            crosswordViewModel.AssignCrosswordDisplayBoard(crosswordBoardMain.Board, CrosswordBoardPlacedWord.Instance().Board);

            crosswordViewModel.AssignDefinitionList(placementManager.Definitions);

            placementManager.ApplyDefinitionNumbersToDisplayBoard(placementManager.GroupedWords, crosswordViewModel.DisplayBoard);

        }
    }
}
