﻿namespace Othello
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
            char columnChar = i_ChosenCell.ToLower().ToCharArray()[0];
            string rowStr = i_ChosenCell.Substring(1);
            int column = columnChar - 'a';

            bool validMove = inputIsCell(columnChar, rowStr, ref row);
            if (!validMove)
            {
                badMsg = string.Format("{0}, The entered word is not a cell in the board!!! Please play again.",
                    i_Player.Name);
            }
            else
            {
                validMove = false;

                for (int i = -1 ; i <= 1 ; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (j != 0 || i != 0)
                        {
                            if (canEat(column, row, i, j, i_Player))
                            {
                                validMove = true;
                                eatCells(column, row, i, j, i_Player);
                            }
                        }
                    }
                }

                if (!validMove)
                {
                    badMsg = string.Format("You can not play on cell {0}", i_ChosenCell);
                }
            }

            return validMove;
        }

        private bool canEat(int i_Row, int i_Column, int i_moveRow, int i_MoveColumn, Player i_Player)
        {
            int i = 1 ;
            bool isCanEatRight = false;

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
            } while (s_Board.Board[i_Column, i_Row] != i_Player.PlayerEnum &&
                     s_Board.Board[i_Column, i_Row] != ePlayers.NoPlayer);
            
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

            } while (s_Board.Board[i_Column, i_Row] != i_Player.PlayerEnum &&
                     s_Board.Board[i_Column, i_Row] != ePlayers.NoPlayer);
        }

        private bool inputIsCell(char i_Column, string i_RowStr , ref int o_Row)
        {
            bool canParse = int.TryParse(i_RowStr, out o_Row);

            if (!canParse)
            {
                canParse = false;
            }
            else if (o_Row < 1 || o_Row > s_Board.Size)
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
