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
        
        public bool TryPlayMove(Player i_Player, string i_ChosenCell, ref string badMsg)
        {
            bool canPlay = false;
            int row = -1;

            char columnChar = i_ChosenCell.ToLower().ToCharArray()[0];
            string rowStr = i_ChosenCell.Substring(1);



            if (!inputIsCell(columnChar, rowStr, ref row))
            {
                badMsg = string.Format("{0}, The entered word is not a cell in the board!!! Please play again.", i_Player.Name);
                return false;
            }
            int column = columnChar - 'a';
            bool isEatRight = canEatRight(column, row, i_Player);
            if (isEatRight)
            {
                int i = 1;
                while (s_Board.Board[row, column + 1] != i_Player.PlayerEnum && s_Board.Board[row, column + 1] != ePlayers.NoPlayer)
                {
                    s_Board.Board[row, column] = i_Player.PlayerEnum;
                }
                return true;
            }
            badMsg = "NONONONONONONONONONONONONONONONO!!!";
            return false;
        }

        private bool canEatRight(int column, int row, Player i_Player)
        {
            int i = 1;

            if (s_Board.Board[row, column + 1] != i_Player.PlayerEnum && s_Board.Board[row, column + 1] != ePlayers.NoPlayer)
            {
                return false;
            }

            while (s_Board.Board[row, column + 1] != i_Player.PlayerEnum && s_Board.Board[row, column + 1] != ePlayers.NoPlayer)
            {
                i++;
            }

            if (s_Board.Board[row, column + i] == i_Player.PlayerEnum)
            {
                return true;
            }

            return false;
        }

        private bool inputIsCell(char i_Column, string i_RowStr , ref int io_row)
        {
            bool canParse;

            if (i_RowStr.Length > 2 || i_RowStr.Length < 1)
            {
                return false;
            }
            
            if (i_Column - 'a' < 0 || i_Column - 'a' >= s_Board.Size)
            {
                return false;
            }
            
            canParse = int.TryParse(i_RowStr, out io_row);
            if(! canParse)
            {
                return false;
            }
            else if (io_row < 1 || io_row > s_Board.Size)
            {
                return false;
            }

            return true;
        }

        public bool IsGameOver()
        {
            return false;
        }

        internal void ShowScore()
        {
            throw new NotImplementedException();
        }
    }
}
