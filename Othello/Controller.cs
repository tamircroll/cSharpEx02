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
            int row = -1;
            bool validMove = false;
            badMsg = string.Empty;

            if (!string.IsNullOrEmpty(i_ChosenCell))
            {
                char columnChar = i_ChosenCell.ToLower().ToCharArray()[0];
                string rowStr = i_ChosenCell.Substring(1);
                int column = columnChar - 'a';
                validMove = inputIsCell(columnChar, rowStr, ref row);
                const bool v_IfValidEat = true;

                if (!validMove)
                {
                    badMsg = string.Format("{0}, The entered word is not a cell in the board!!! Please play again.", i_Player.Name);
                }
                else
                {
                    validMove = checkValidMoveAndEat(column, row, i_Player, v_IfValidEat);

                    if (!validMove)
                    {
                        badMsg = string.Format("You can not play on cell {0}", i_ChosenCell);
                    }
                }
            }

            return validMove;
        }

        private bool checkValidMoveAndEat(int i_Column, int i_Row, Player i_Player, bool ifValidEat)
        {
            bool validMove = false;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (j != 0 || i != 0)
                    {
                        if (canEat(i_Column, i_Row, i, j, i_Player))
                        {
                            validMove = true;
                            if (ifValidEat)
                            {
                                eatCells(i_Column, i_Row, i, j, i_Player);
                            }
                        }
                    }
                }
            }

            return validMove;
        }

        private bool canEat(int i_Row, int i_Column, int i_moveRow, int i_MoveColumn, Player i_Player)
        {
            int i = 1;
            bool isCanEatRight = false;

            if (s_Board.Board[i_Column, i_Row] == ePlayers.NoPlayer)
            {
                do
                {
                    i_Row += i_moveRow;
                    i_Column += i_MoveColumn;

                    if (i_Row < 0 || i_Column < 0 || i_Row >= s_Board.Size || i_Column >= s_Board.Size)
                    {
                        break;
                    }

                    if (s_Board.Board[i_Column, i_Row] == i_Player.PlayerEnum)
                    {
                        isCanEatRight = i > 1;
                        break;
                    }

                    i++;
                } 
                while (s_Board.Board[i_Column, i_Row] != i_Player.PlayerEnum &&
                         s_Board.Board[i_Column, i_Row] != ePlayers.NoPlayer);
            }

            return isCanEatRight;
        }

        private void eatCells(int i_Row, int i_Column, int i_moveRow, int i_MoveColumn, Player i_Player)
        {
            do
            {
                s_Board.Board[i_Column, i_Row] = i_Player.PlayerEnum;

                i_Row += i_moveRow;
                i_Column += i_MoveColumn;

                if (s_Board.Board[i_Column, i_Row] == i_Player.PlayerEnum)
                {
                    break;
                }
            }
            while (s_Board.Board[i_Column, i_Row] != i_Player.PlayerEnum && s_Board.Board[i_Column, i_Row] != ePlayers.NoPlayer);
        }

        private bool inputIsCell(char i_Column, string i_RowStr, ref int o_Row)
        {
            bool canParse = int.TryParse(i_RowStr, out o_Row);

            if (o_Row < 1 || o_Row > s_Board.Size)
            {
                canParse = false;
            }
            else if (i_RowStr.Length > 2 || i_RowStr.Length < 1)
            {
                canParse = false;
            }
            else if (i_Column - 'a' < 0 || i_Column - 'a' >= s_Board.Size)
            {
                canParse = false;
            }

            o_Row--;
            
            return canParse;
        }

        public bool CanPlayerPlay(Player i_Player)
        {
            const bool v_IfValidEat = true;
            bool validMove, canPlay = false;

            for (int i = 0; i < s_Board.Size; i++)
            {
                for (int j = 0; j < s_Board.Size; j++)
                {
                    validMove = checkValidMoveAndEat(i, j, i_Player, !v_IfValidEat);
                    if (validMove)
                    {
                        canPlay = true;
                        break;
                    }
                }

                if (canPlay)
                {
                    break;
                }
            }

            return canPlay;
        }

        internal void CalcAndShowScore()
        {
            int firstPlayer = 0, secondPlayer = 0;
            for (int i = 0; i < s_Board.Size; i++)
            {
                for (int j = 0; j < s_Board.Size; j++)
                {
                    if (s_Board.Board[i, j] == ePlayers.Player1)
                    {
                        firstPlayer++;
                    }
                    else if (s_Board.Board[i, j] == ePlayers.Player2)
                    {
                        secondPlayer++;
                    }
                }
            }

            Ex02.ConsoleUtils.Screen.Clear();
            View.DrawBoard(s_Board);
            View.ShowScore(firstPlayer, secondPlayer);
        }
    }
}
