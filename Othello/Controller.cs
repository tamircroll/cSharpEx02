namespace Othello
{
    using System.Collections.Generic;

    public class Controller
    {
        private GameBoard m_Board;

        public Controller(GameBoard i_SBoard)
        {
            m_Board = i_SBoard;
        }
        
        public bool TryPlayMove(Player i_Player, string i_ChosenCell, ref string o_Msg)
        {
            int row;
            bool validMove = false;

            if (!string.IsNullOrEmpty(i_ChosenCell))
            {
                char columnChar = i_ChosenCell.ToLower().ToCharArray()[0];
                string rowStr = i_ChosenCell.Substring(1);
                int column = columnChar - 'a';
                validMove = inputIsCell(rowStr, columnChar, out row);

                if (!validMove)
                {
                    o_Msg = string.Format("{0}, The entered word is not a cell in the board!!! Please play again.", i_Player.Name);
                }
                else
                {
                    validMove = isMoveInValidMovesList(row, column, i_Player);

                    if (!validMove)
                    {
                        o_Msg = string.Format("{0}, You can not choose on cell {1}, Please try again.", i_Player.Name, i_ChosenCell);
                    }
                    else
                    {
                        executePlayMove(row, column, i_Player);
                    }
                }
            }

            return validMove;
        }

        private bool isMoveInValidMovesList(int i_Row, int i_Column, Player i_Player)
        {
            return i_Player.ValidateMoves.Contains(string.Format("{0},{1}", i_Row, i_Column));
        }

        public void executePlayMove(int i_Row, int i_Column, Player i_Player)
        {
            for (int rowMoveDirection = -1; rowMoveDirection <= 1; rowMoveDirection++)
            {
                for (int columnMoveDirection = -1; columnMoveDirection <= 1; columnMoveDirection++)
                {
                    if (columnMoveDirection != 0 || rowMoveDirection != 0)
                    {
                        if (canEat(i_Row, i_Column, rowMoveDirection, columnMoveDirection, i_Player))
                        {
                            eatPiecesInDirection(i_Row, i_Column, rowMoveDirection, columnMoveDirection, i_Player);
                        }
                    }
                }
            }

            m_Board.Board[i_Row, i_Column] = i_Player.PlayerEnum;
        }

        private void eatPiecesInDirection(int i_Row, int i_Column, int i_moveRow, int i_MoveColumn, Player i_Player)
        {
            do
            {
                i_Row += i_moveRow;
                i_Column += i_MoveColumn;
                m_Board.Board[i_Row, i_Column] = i_Player.PlayerEnum;
            }
            while (m_Board.Board[i_Row + i_moveRow, i_Column + i_MoveColumn] != i_Player.PlayerEnum);
        }

        private bool canEat(int i_Row, int i_Column, int i_RowDirection, int i_ColumnDirection, Player i_Player)
        {
            int numOfPiecesToEat = 0;
            bool canEat = false;

            if (m_Board.Board[i_Row, i_Column] == ePlayers.NoPlayer)
            {
                do
                {
                    i_Row += i_RowDirection;
                    i_Column += i_ColumnDirection;

                    if (i_Row < 0 || i_Column < 0 || i_Row >= m_Board.Size || i_Column >= m_Board.Size)
                    {
                        break;
                    }

                    if (m_Board.Board[i_Row, i_Column] == i_Player.PlayerEnum)
                    {
                        canEat = numOfPiecesToEat > 0;
                        break;
                    }

                    numOfPiecesToEat++;
                }
                while (m_Board.Board[i_Row, i_Column] != ePlayers.NoPlayer);
            }

            return canEat;
        }

        private bool IsValidMove(int i_Row, int i_Column, Player i_Player)
        {
            bool validMove = false;

            for (int rowDirection = -1; rowDirection <= 1; rowDirection++)
            {
                for (int columnDirection = -1; columnDirection <= 1; columnDirection++)
                {
                    if (columnDirection != 0 || rowDirection != 0)
                    {
                        if (canEat(i_Row, i_Column, rowDirection, columnDirection, i_Player))
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

        private bool inputIsCell(string i_RowStr, char i_Column, out int o_Row)
        {
            bool canParse = int.TryParse(i_RowStr, out o_Row);

            if (canParse)
            {
                if (o_Row < 1 || o_Row > m_Board.Size)
                {
                    canParse = false;
                }
                else if (i_Column - 'a' < 0 || i_Column - 'a' >= m_Board.Size)
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

            for (int row = 0; row < m_Board.Size; row++)
            {
                for (int column = 0; column < m_Board.Size; column++)
                {
                    validMove = IsValidMove(row, column, i_Player);
                    if(validMove)
                    {
                        validateMoves.Add(string.Format("{0},{1}", row, column));
                    }
                }
            }

            i_Player.ValidateMoves = validateMoves;

            return validateMoves.Count != 0;
        }

        internal void CalcScore(Player i_Player1, Player i_Player2)
        {
            i_Player1.m_Score = 0;
            i_Player2.m_Score = 0;

            for (int row = 0; row < m_Board.Size; row++)
            {
                for (int column = 0; column < m_Board.Size; column++)
                {
                    if (m_Board.Board[row, column] == ePlayers.Player1)
                    {
                        i_Player1.m_Score++;
                    }
                    else if (m_Board.Board[row, column] == ePlayers.Player2)
                    {
                        i_Player2.m_Score++;
                    }
                }
            }
        }
    }
}
