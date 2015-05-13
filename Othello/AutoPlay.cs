namespace Othello
{
    using System;

    public class AutoPlay
    {
        private const int k_Row = 0, k_Column = 1;

        internal static void PlayRandom(Player i_Player, GameBoard i_Board)
        {
            int rndIndex = new Random().Next(i_Player.GetValidateMoves(i_Board).Count);
            string rndCellStr = i_Player.GetValidateMoves(i_Board)[rndIndex];
            int[] rowAndCol = getRowAndCol(rndCellStr); 

            Controller.ExecutePlayMove(rowAndCol[k_Row], rowAndCol[k_Column], i_Player, i_Board);
        }

        internal static void SmartPlay(Player i_AutoPlayer, Player i_Rival, GameBoard i_Board, int i_RecDepth)
        {
            string bestMove = null;
            int bestScore = 0;
            int[] rowAndCol;

            foreach (string move in i_AutoPlayer.GetValidateMoves(i_Board))
            {
                GameBoard clonedBoard = i_Board.CloneBoard();
                rowAndCol = getRowAndCol(move);

                Controller.ExecutePlayMove(rowAndCol[k_Row], rowAndCol[k_Column], i_AutoPlayer, clonedBoard);
                int curScore = calcRecursiveMoveMinMax(i_AutoPlayer, i_Rival, clonedBoard, i_RecDepth);

                if (bestScore <= curScore)
                {
                    bestMove = move;
                    bestScore = curScore;
                }
            }

            rowAndCol = getRowAndCol(bestMove);
            Controller.ExecutePlayMove(rowAndCol[k_Row], rowAndCol[k_Column], i_AutoPlayer, i_Board);
        }

        private static int calcRecursiveMoveMinMax(Player i_AutoPlayer, Player i_Rival, GameBoard i_Board, int i_RecDepth)
        {
            int minScore = int.MaxValue;

            foreach (string rivalMove in i_Rival.GetValidateMoves(i_Board))
            {
                int[] rowAndCol = getRowAndCol(rivalMove);
                GameBoard boardClone = i_Board.CloneBoard();
                Controller.ExecutePlayMove(rowAndCol[k_Row], rowAndCol[k_Column], i_Rival, boardClone);
                int curScore = recursiveHelper(i_AutoPlayer, i_Rival, boardClone, i_RecDepth - 1);

                if (minScore > curScore)
                {
                    minScore = curScore;
                }
            }

            if (minScore == int.MaxValue)
            {
                minScore = recursiveHelper(i_AutoPlayer, i_Rival, i_Board, i_RecDepth - 1);
            }

            return minScore;
        }

        private static int recursiveHelper(Player i_AutoPlayer, Player i_Rival, GameBoard i_Board, int i_RecDepth)
        {
            int maxScore = 0, curScore = 0;

            foreach (string playerlMove in i_AutoPlayer.GetValidateMoves(i_Board))
            {
                GameBoard boardClone = i_Board.CloneBoard();
                int[] rowAndCol = getRowAndCol(playerlMove);
                Controller.ExecutePlayMove(rowAndCol[k_Row], rowAndCol[k_Column], i_AutoPlayer, boardClone);

                if (i_RecDepth == 0)
                {
                    curScore = calcScore(i_AutoPlayer, boardClone, rowAndCol);
                }
                else
                {
                    curScore = calcRecursiveMoveMinMax(i_AutoPlayer, i_Rival, boardClone, i_RecDepth);
                }

                if (maxScore < curScore)
                {
                    maxScore = curScore;
                }
            }

            if (i_RecDepth == 0 && maxScore == 0)
            {
                maxScore = int.MaxValue;
            }

            return maxScore;
        }

        private static int calcScore(Player i_AutoPlayer, GameBoard boardClone, int[] i_RowAndCol)
        {
            int corner = 1;
            int score = i_AutoPlayer.Score(boardClone);
            int flexability = i_AutoPlayer.GetValidateMoves(boardClone).Count;

            if (isCorner(boardClone, i_RowAndCol))
            {
                corner = 2;
            }

            return (score + flexability) * corner;
        }

        private static bool isCorner(GameBoard i_BoardClone, int[] i_RowAndCol)
        {
            return (i_RowAndCol[k_Row] == 0 && i_RowAndCol[k_Column] == 0) ||
                   (i_RowAndCol[k_Row] == i_BoardClone.Size - 1 && i_RowAndCol[k_Column] == 0) ||
                   (i_RowAndCol[k_Row] == i_BoardClone.Size - 1 && i_RowAndCol[k_Column] == i_BoardClone.Size - 1) ||
                   (i_RowAndCol[k_Row] == 0 && i_RowAndCol[k_Column] == i_BoardClone.Size - 1);
        }

        private static int[] getRowAndCol(string i_CellStr)
        {
            int row, col;
            string[] cellsStr = i_CellStr.Split(',');
            int.TryParse(cellsStr[0], out row);
            int.TryParse(cellsStr[1], out col);

            return new[] { row, col };
        }
    }
}
