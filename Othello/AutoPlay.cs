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

        internal static void RecursiveCalculation(Player i_Player, Player i_Rival, GameBoard i_Board, int count)
        {
            string[] bestScore = null;

            foreach (var move in i_Player.ValidateMoves)
            {
                string[] curScore = RecursiveCalculation(i_Player.ClonePlayer(), i_Rival.ClonePlayer(), move, i_Board.CloneBoard(), count);

                if (bestScore == null)
                {
                    bestScore = curScore;
                }

                bestScore = getBetterScore(bestScore, curScore);
            }

            int[] rowAndCol = getRowAndCol(bestScore[0]);
            Controller.ExecutePlayMove(rowAndCol[0], rowAndCol[1], i_Player, i_Board);
        }

        internal static string[] RecursiveCalculation(Player i_Player, Player i_Rival, string i_Move, GameBoard i_Board, int count)
        {
            string[] bestScore = null;

            Controller.ListAllPossibleMoves(i_Player, i_Board);
            if (count == 0 || i_Player.ValidateMoves.Count == 0)
            {
                Controller.CalcScore(i_Rival, i_Player, i_Board);
                return new string[] { i_Move, i_Player.m_Score.ToString() };
            }

            bestScore = recursiveHelper(i_Player, i_Rival, i_Board, count);

            return bestScore;
        }

        private static string[] recursiveHelper(Player i_Player, Player i_Rival, GameBoard i_Board, int count)
        {
            GameBoard board = i_Board.CloneBoard();
            Player rivalBestMove = i_Rival.ClonePlayer();
            string[] bestScore = null;
            Controller.CalcScore(rivalBestMove, i_Player, board);
            
            foreach (string rivalMove in i_Rival.ValidateMoves)
            {
                Player rivalClone = i_Rival.ClonePlayer();
                GameBoard boardClone = i_Board.CloneBoard();

                Controller.TryPlayMove(rivalClone, rivalMove, boardClone);
                Controller.ListAllPossibleMoves(rivalClone, boardClone);
                Controller.CalcScore(rivalClone, i_Player, boardClone);

                if (rivalClone.m_Score > rivalBestMove.m_Score)
                {
                    rivalBestMove = rivalClone;
                }
            }

            bestScore = recursiveHelper2(count, i_Player.ClonePlayer(), rivalBestMove.ClonePlayer(), board);
            if (bestScore == null)
            {
                bestScore = recursiveHelper2(count, i_Player, i_Rival, i_Board);
            }

            return bestScore;
        }

        private static string[] recursiveHelper2(int count, Player playerClone, Player rivalClone, GameBoard boardClone)
        {
            string[] bestScore = null;

            foreach (string playerlMove in playerClone.ValidateMoves)
            {
                Player playerClone2 = playerClone.ClonePlayer();
                Player rivalClone2 = rivalClone.ClonePlayer();
                GameBoard boardClone2 = boardClone.CloneBoard();

                Controller.TryPlayMove(playerClone2, playerlMove, boardClone2);
                Controller.ListAllPossibleMoves(playerClone2, boardClone2);
                Controller.ListAllPossibleMoves(rivalClone2, boardClone2);

                string[] curScore = RecursiveCalculation(playerClone2.ClonePlayer(), rivalClone2.ClonePlayer(), playerlMove, boardClone2.CloneBoard(), count - 1);

                if (bestScore == null)
                {
                    bestScore = curScore;
                }

                bestScore = getBetterScore(bestScore, curScore);
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

        private static int[] getRowAndCol(string i_CellStr)
        {
            char[] cellCharArr = i_CellStr.ToCharArray();

            return new int[] { cellCharArr[0] - '0', cellCharArr[2] - '0' };
        }
    }
}
