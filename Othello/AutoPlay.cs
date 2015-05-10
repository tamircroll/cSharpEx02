namespace Othello
{
    using System;

    public class AutoPlay
    {
        private const int k_Row = 0;
        private const int k_Column = 1;

        internal static void PlayRandom(Player i_Player, GameBoard i_Board)
        {
            int rndIndex = new Random().Next(i_Player.ValidateMoves.Count);
            string rndCellStr = i_Player.ValidateMoves[rndIndex];
            int[] rowAndCol = getRowAndCol(rndCellStr); 

            Controller.ExecutePlayMove(rowAndCol[k_Row], rowAndCol[k_Column], i_Player, i_Board);
        }

        internal static void SmartPlay(Player i_AutoPlayer, Player i_Rival, GameBoard i_Board, int iRecDepth)
        {
            string[] bestScore = null;

            foreach (string move in i_AutoPlayer.ValidateMoves)
            {
                GameBoard clonedBoard = i_Board.CloneBoard();
                Controller.TryPlayMove(i_AutoPlayer, move, clonedBoard);

                string[] curScore = calcMoveMinMax(i_AutoPlayer.ClonePlayer(), i_Rival.ClonePlayer(), clonedBoard, iRecDepth);
                if (bestScore == null)
                {
                    bestScore = curScore;
                }

                bestScore = getBetterScore(bestScore, curScore);
            }

            int[] rowAndCol = getRowAndCol(bestScore[0]);
            Controller.ExecutePlayMove(rowAndCol[0], rowAndCol[1], i_AutoPlayer, i_Board);
        }

        private static string[] calcMoveMinMax(Player i_AutoPlayer, Player i_Rival, GameBoard i_Board, int iRecDepth)
        {
            string[] toRetuen = null;
            
            foreach (string rivalMove in i_Rival.ValidateMoves)
            {
                Player playerClone = i_AutoPlayer.ClonePlayer();
                Player rivalClone = i_Rival.ClonePlayer();
                GameBoard boardClone = i_Board.CloneBoard();

                Controller.TryPlayMove(rivalClone, rivalMove, boardClone);
                Controller.ListAllPossibleMoves(playerClone, boardClone);

                string[] curScore = calcMoveHelper(i_AutoPlayer.ClonePlayer(), rivalClone, boardClone.CloneBoard(), iRecDepth - 1);
                if (toRetuen == null)
                {
                    toRetuen = curScore;
                }

                toRetuen = getWorstScore(toRetuen, curScore);
            }

            if (toRetuen == null)
            {
                toRetuen = calcMoveHelper(i_AutoPlayer.ClonePlayer(), i_Rival.ClonePlayer(), i_Board.CloneBoard(), iRecDepth - 1);
            }

            return toRetuen;
        }

        private static string[] calcMoveHelper(Player i_AutoPlayer, Player i_Rival, GameBoard i_Board, int iRecDepth)
        {
            string[] bestScore = null;

            foreach (string playerlMove in i_AutoPlayer.ValidateMoves)
            {
                Player playerClone = i_AutoPlayer.ClonePlayer();
                GameBoard boardClone = i_Board.CloneBoard();

                Controller.TryPlayMove(playerClone, playerlMove, boardClone);
                Controller.ListAllPossibleMoves(playerClone, boardClone);
                string[] curScore = { playerlMove, (playerClone.Score + playerClone.ValidateMoves.Count).ToString() };

                if (bestScore == null)
                {
                    bestScore = curScore;
                }

                Controller.UpdatePlayersScore(i_Rival, playerClone, boardClone);

                bestScore = (iRecDepth == 0)
                    ? getBetterScore(bestScore, curScore)
                    : calcMoveMinMax(playerClone.ClonePlayer(), i_Rival.ClonePlayer(), boardClone.CloneBoard(), iRecDepth);
            }

            return bestScore;
        }

        private static string[] getBetterScore(string[] i_BestScore, string[] i_CurScore)
        {
            int bestScore, curScore;

            int.TryParse(i_BestScore[1], out bestScore);
            int.TryParse(i_CurScore[1], out curScore);

            return (bestScore > curScore) ? i_BestScore : i_CurScore;
        }

        private static string[] getWorstScore(string[] i_BestScore, string[] i_CurScore)
        {
            int bestScore, curScore;
            int.TryParse(i_BestScore[1], out bestScore);
            int.TryParse(i_CurScore[1], out curScore);

            return (bestScore < curScore) ? i_BestScore : i_CurScore;
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
