using System;
using System.Collections.Generic;

namespace Othello
{
    public class i_GameBoard
    {
        private readonly int r_Size;
        private readonly ePlayers[,] r_Board;
        private DateTime? m_LastUpdate;
  
        public i_GameBoard(int i_Size)
        {
            r_Size = i_Size;
            r_Board = new ePlayers[i_Size, i_Size];
            m_LastUpdate = DateTime.Now;

            r_Board[i_Size / 2, i_Size / 2] = ePlayers.Player1;
            r_Board[(i_Size / 2) - 1, (i_Size / 2) - 1] = ePlayers.Player1;
            r_Board[i_Size / 2, (i_Size / 2) - 1] = ePlayers.Player2;
            r_Board[(i_Size / 2) - 1, i_Size / 2] = ePlayers.Player2;
        }

        public ePlayers this[int i_X, int i_Y]
        {
            get
            {
                return r_Board[i_X, i_Y];
            }

            set
            {
                m_LastUpdate = DateTime.Now;
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

        public ePlayers[,] Board
        {
            get { return r_Board; }
        }

        public i_GameBoard CloneBoard()
        {
            i_GameBoard cloned = new i_GameBoard(Size);

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
