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
        
        public bool TryPlayMove(Player i_Player, string i_ChosenCell, ref string o_Msg)
        {
            int row = -1;
            bool validMove = false;

            if (!string.IsNullOrEmpty(i_ChosenCell))
            {
                char columnChar = i_ChosenCell.ToLower().ToCharArray()[0];
                string rowStr = i_ChosenCell.Substring(1);
                int column = columnChar - 'a';
                validMove = inputIsCell(rowStr, columnChar, ref row);

                if (!validMove)
                {
                    o_Msg = string.Format("{0}, The entered word is not a cell in the board!!! Please play again.", i_Player.Name);
                }
                else
                {
                    validMove = isMoveInValidMovesList(row, column, i_Player);

                    if (!validMove)
                    {
                        o_Msg = string.Format("{0}, You can not play on cell {1}, Please try again.", i_Player.Name, i_ChosenCell);
                    }
                    else
                    {
                        executePlay(row, column, i_Player);
                    }
                }
            }

            return validMove;
        }

        private bool isMoveInValidMovesList(int i_Row, int i_Column, Player i_Player)
        {
            return i_Player.ValidateMoves.Contains(string.Format("{0},{1}", i_Row, i_Column));
        }

        public void executePlay(int i_Row, int i_Column, Player i_Player)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (j != 0 || i != 0)
                    {
                        if (canEat(i_Row, i_Column, i, j, i_Player))
                        {
                            eatPieces(i_Row, i_Column, i, j, i_Player);
                        }
                    }
                }
            }

            s_Board.Board[i_Row, i_Column] = i_Player.PlayerEnum;
        }

        private void eatPieces(int i_Row, int i_Column, int i_moveRow, int i_MoveColumn, Player i_Player)
        {
            do
            {
                i_Row += i_moveRow;
                i_Column += i_MoveColumn;
                s_Board.Board[i_Row, i_Column] = i_Player.PlayerEnum;
            }
            while (s_Board.Board[i_Row + i_moveRow, i_Column + i_MoveColumn] != i_Player.PlayerEnum);
        }

        private bool canEat(int i_Row, int i_Column, int i_moveRow, int i_MoveColumn, Player i_Player)
        {
            int numOfPiecesToEat = 0;
            bool canEat = false;

            if (s_Board.Board[i_Row, i_Column] == ePlayers.NoPlayer)
            {
                do
                {
                    i_Row += i_moveRow;
                    i_Column += i_MoveColumn;

                    if (i_Row < 0 || i_Column < 0 || i_Row >= s_Board.Size || i_Column >= s_Board.Size)
                    {
                        break;
                    }

                    if (s_Board.Board[i_Row, i_Column] == i_Player.PlayerEnum)
                    {
                        canEat = numOfPiecesToEat > 0;
                        break;
                    }

                    numOfPiecesToEat++;
                }
                while (s_Board.Board[i_Row, i_Column] != ePlayers.NoPlayer);
            }

            return canEat;
        }

        private bool IsValidMove(int i_Row, int i_Column, Player i_Player)
        {
            bool validMove = false;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (j != 0 || i != 0)
                    {
                        if (canEat(i_Row, i_Column, i, j, i_Player))
                        {
                            validMove = true;
                            break;
                        }
                    }
                }

                if (validMove)
                {
                    break;
                }
            }

            return validMove;
        }

        private bool inputIsCell(string i_RowStr, char i_Column, ref int o_Row)
        {
            bool canParse = int.TryParse(i_RowStr, out o_Row);

            if (canParse)
            {
                if (o_Row < 1 || o_Row > s_Board.Size)
                {
                    canParse = false;
                }
                else if (i_Column - 'a' < 0 || i_Column - 'a' >= s_Board.Size)
                {
                    canParse = false;
                }

                o_Row--;
            }

            return canParse;
        }

        public bool ListAllPossibleMoves(Player i_Player)
        {
            bool validMove;
            List<string> validateMoves = new List<string>();

            for (int i = 0; i < s_Board.Size; i++)
            {
                for (int j = 0; j < s_Board.Size; j++)
                {
                    validMove = IsValidMove(i, j, i_Player);
                    if(validMove)
                    {
                        validateMoves.Add(string.Format("{0},{1}", i, j));
                    }
                }
            }

            i_Player.ValidateMoves = validateMoves;

            return validateMoves.Count != 0;
        }

        internal void CalcAndShowScore(Player i_Player1, Player i_Player2)
        {
            int firstPlayer = 0, secondPlayer = 0;
            Player winner = null;

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

            i_Player1.m_Score = firstPlayer;
            i_Player2.m_Score = secondPlayer;

            if (firstPlayer > secondPlayer)
            {
                winner = i_Player1;
            }
            else if (secondPlayer > firstPlayer)
            {
                winner = i_Player2;
            }

            Ex02.ConsoleUtils.Screen.Clear();
            View.DrawBoard(s_Board);
            View.ShowScore(i_Player1, i_Player2, winner);
        }
    }
}
