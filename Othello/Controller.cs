namespace Othello
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Controller
    {
        private static GameBoard s_Board;

        public Controller(GameBoard iSBoard)
        {
            s_Board = iSBoard;
        }
        
        public bool PlayMove(ePlayers i_Player, string i_ChosenCell)
        {
            bool canPlay = false;



            return canPlay;
        }
        
        public static bool IsGameOver()
        {
            return false;
        }

        private bool isRight()
        {
        }
    }
}
