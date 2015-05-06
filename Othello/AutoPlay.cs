namespace Othello
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AutoPlay
    {
        internal static void PlayRandom(Player i_Player, Controller i_controller)
        {
            int rndIndex = new Random().Next(i_Player.ValidateMoves.Count);
            string rndCellStr = i_Player.ValidateMoves[rndIndex];
            int row = rndCellStr.ToCharArray()[0] - '0';
            int column = rndCellStr.ToCharArray()[2] - '0';

            i_controller.executePlayMove(row, column, i_Player);
        }

        public static void PlayRandom2(Player i_Player, Controller i_controller)
        {
        }
    }
}
