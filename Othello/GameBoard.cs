namespace Othello
{
    public class GameBoard
    {
        private readonly int r_Size;
        private ePlayers[,] m_Board;

        public GameBoard(int i_Size)
        {
            r_Size = i_Size;
            m_Board = new ePlayers[i_Size, i_Size];

            m_Board[i_Size / 2, i_Size / 2] = ePlayers.Player1;
            m_Board[(i_Size / 2) - 1, (i_Size / 2) - 1] = ePlayers.Player1;
            m_Board[i_Size / 2, (i_Size / 2) - 1] = ePlayers.Player2;
            m_Board[(i_Size / 2) - 1, i_Size / 2] = ePlayers.Player2;
        }

        public int Size
        {
            get
            {
                return r_Size;
            }
        }

        public ePlayers[,] Board
        {
            get
            {
                return m_Board;
            }
        }
    }
}
