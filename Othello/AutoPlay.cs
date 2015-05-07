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

            Controller.executePlayMove(rowAndCol[k_Row], rowAndCol[k_Column], i_Player, i_Board);
        }

        internal static void PlayCalculatedMove(Player i_Player, Controller i_controller, GameBoard i_Board)
        {
        }

        private static int[] getRowAndCol(string i_CellStr)
        {
            char[] cellCharArr = i_CellStr.ToCharArray();

            return new int[] { cellCharArr[0] - '0', cellCharArr[2] - '0' };
        }
    }
}
