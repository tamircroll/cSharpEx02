using System;

namespace Othello
{
    using System.Collections.Generic;

    public struct Player
    {
        private readonly string r_Name;
        private readonly ePlayers r_PlayerEnum;
        private DateTime? m_LastUpdateBoard;
        private List<string> m_ValidateMoves;

        public Player(string i_Name, ePlayers i_PlayerEnum, i_GameBoard i_Board)
        {
            r_Name = i_Name;
            r_PlayerEnum = i_PlayerEnum;
            m_LastUpdateBoard = i_Board.LastUpdate;
            m_ValidateMoves = new List<string>();
            m_ValidateMoves = Controller.ListAllPossibleMoves(this, i_Board);
        }

        public int Score(i_GameBoard i_Board)
        {
            int m_Score = 0;

            for (int row = 0; row < i_Board.Size; row++)
            {
                for (int column = 0; column < i_Board.Size; column++)
                {
                    if (i_Board[row, column] == r_PlayerEnum)
                    {
                        m_Score++;
                    }
                }
            }

            return m_Score;
        }

        public List<string> ValidateMoves
        {
            set { m_ValidateMoves = value; }
        }

        public List<string> GetValidateMoves(i_GameBoard i_Board)
        {
            if (m_LastUpdateBoard != i_Board.LastUpdate)
            {
                m_LastUpdateBoard = i_Board.LastUpdate;
                m_ValidateMoves = Controller.ListAllPossibleMoves(this, i_Board);
            }

            return m_ValidateMoves;
        }

        public string Name 
        {
            get { return r_Name; }
        }

        public ePlayers PlayerEnum
        {
            get { return r_PlayerEnum; }
        }
    }
}
