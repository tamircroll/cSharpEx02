using System;

namespace Othello
{
    public class GameBoard
    {
        private readonly int r_Size;
        private readonly ePlayer[,] r_Board;
        private DateTime? m_LastUpdate;
        private int m_PlayerOneScore = 2, m_PlayerTwoScore = 2;
  
        public GameBoard(int i_Size)
        {
            r_Size = i_Size;
            r_Board = new ePlayer[i_Size, i_Size];
            m_LastUpdate = DateTime.Now;

            r_Board[i_Size / 2, i_Size / 2] = ePlayer.Player1;
            r_Board[(i_Size / 2) - 1, (i_Size / 2) - 1] = ePlayer.Player1;
            r_Board[i_Size / 2, (i_Size / 2) - 1] = ePlayer.Player2;
            r_Board[(i_Size / 2) - 1, i_Size / 2] = ePlayer.Player2;
        }

        public ePlayer this[int i_X, int i_Y]
        {
            get
            {
                return r_Board[i_X, i_Y];
            }

            set
            {
                m_LastUpdate = DateTime.Now;
                if (value == ePlayer.Player1 && r_Board[i_X, i_Y] == ePlayer.Player2)
                {
                    m_PlayerOneScore++;
                    m_PlayerTwoScore--;
                }
                else if (value == ePlayer.Player2 && r_Board[i_X, i_Y] == ePlayer.Player1)
                {
                    m_PlayerOneScore--;
                    m_PlayerTwoScore++;
                }
                else if (value == ePlayer.Player1 && r_Board[i_X, i_Y] == ePlayer.NoPlayer)
                {
                    m_PlayerOneScore++;
                }
                else if (value == ePlayer.Player2 && r_Board[i_X, i_Y] == ePlayer.NoPlayer)
                {
                    m_PlayerTwoScore++;
                }

                r_Board[i_X, i_Y] = value;
            }
        }

        public DateTime? LastUpdate
        {
            get { return m_LastUpdate; }
        }

        public int Size
        {
            get { return r_Size; }
        }

        public ePlayer[,] Board
        {
            get { return r_Board; }
        }

        public int GetScore(ePlayer i_Player)
        {
            if (i_Player == ePlayer.Player1)
            {
                return m_PlayerOneScore;
            }
            else
            {
                return m_PlayerTwoScore;
            }
        }

        public GameBoard CloneBoard()
        {
            GameBoard cloned = new GameBoard(Size);
            cloned.m_PlayerOneScore = m_PlayerOneScore;
            cloned.m_PlayerTwoScore = m_PlayerTwoScore;

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    cloned[i, j] = this[i, j];
                }
            }

            return cloned;
        }
    }
}
