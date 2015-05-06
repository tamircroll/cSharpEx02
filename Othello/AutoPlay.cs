namespace Othello
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AutoPlay
    {
        public static void Play(Player i_Player, Player i_Rival, GameBoard i_Board, Controller i_controller)
        {
            int rndIndex = new Random().Next(i_Player.ValidateMoves.Count);
            string rndCellStr = i_Player.ValidateMoves[rndIndex];
            int row = rndCellStr.ToCharArray()[0] - '0';
            int column = rndCellStr.ToCharArray()[2] - '0';

            i_controller.executePlay(row, column, i_Player);
        }
    }
}
