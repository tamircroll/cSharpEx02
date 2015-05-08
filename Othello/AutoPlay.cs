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

        internal static void RecursiveCalculation(Player i_Player, Player i_Rival, GameBoard i_Board)
        {
            string[] bestScore = null;

            foreach (string move in i_Player.ValidateMoves)
            {
                string[] curScore = RecursiveCalculation(i_Player.ClonePlayer(), i_Rival.ClonePlayer(), move, i_Board.CloneBoard());

                if (bestScore == null)
                {
                    bestScore = recursiveHelper(i_Player, i_Rival, i_Board.CloneBoard()); ;
                }

                bestScore = getBetterScore(bestScore, curScore);
            }

            int[] rowAndCol = getRowAndCol(bestScore[0]);
            Controller.ExecutePlayMove(rowAndCol[0], rowAndCol[1], i_Player, i_Board);
        }

        internal static string[] RecursiveCalculation(Player i_Player, Player i_Rival, string i_Move, GameBoard i_Board)
        {
            string[] bestScore = null;
            Controller.TryPlayMove(i_Player, i_Move, i_Board); 
            bestScore = recursiveHelper(i_Player, i_Rival, i_Board);

            return bestScore;
        }

        private static string[] recursiveHelper(Player i_Player, Player i_Rival, GameBoard i_Board)
        {
            string[] toRetuen = null;
            
            foreach (string rivalMove in i_Rival.ValidateMoves)
            {
                Player playerClone = i_Player.ClonePlayer();
                Player rivalClone = i_Rival.ClonePlayer();
                GameBoard boardClone = i_Board.CloneBoard();

                Controller.TryPlayMove(rivalClone, rivalMove, boardClone);
                Controller.ListAllPossibleMoves(playerClone, boardClone);
                Controller.ListAllPossibleMoves(rivalClone, boardClone);
                Controller.CalcScore(rivalClone, i_Player, boardClone);

                toRetuen = recursiveHelper2(i_Player.ClonePlayer(), rivalClone, boardClone.CloneBoard());
                if (toRetuen == null)
                {
                    toRetuen = recursiveHelper2(playerClone,rivalClone, boardClone);
                }
            }

            if (toRetuen == null)
            {
                toRetuen = recursiveHelper2(i_Player, i_Rival, i_Board);
            }

            return toRetuen;
        }

        private static string[] recursiveHelper2(Player i_Player, Player i_Rival, GameBoard i_Board)
        {
            string[] bestScore = null;

            foreach (string playerlMove in i_Player.ValidateMoves)
            {
                Player playerClone = i_Player.ClonePlayer();
                GameBoard boardClone2 = i_Board.CloneBoard();

                Controller.TryPlayMove(playerClone, playerlMove, boardClone2);
                Controller.ListAllPossibleMoves(playerClone, boardClone2);
                string[] curScore = {playerlMove, playerClone.m_Score.ToString()};

                if (bestScore == null)
                {
                    bestScore = curScore;
                }

                Controller.CalcScore(i_Rival, playerClone, i_Board);

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

        private static string[] getWorstScore(string[] i_BestScore, string[] i_CurScore)
        {
            int bestScore, curScore;
            int.TryParse(i_BestScore[1], out bestScore);
            int.TryParse(i_CurScore[1], out curScore);

            return (bestScore < curScore) ? i_BestScore : i_CurScore;
        }

        private static int[] getRowAndCol(string i_CellStr)
        {
            char[] cellCharArr = i_CellStr.ToCharArray();

            return new int[] { cellCharArr[0] - '0', cellCharArr[2] - '0' };
        }
    }
}
