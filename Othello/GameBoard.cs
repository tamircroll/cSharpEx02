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

        public ePlayers this[int i_X, int i_Y]
        {
            get { return m_Board[i_X, i_Y]; }
            set { m_Board[i_X, i_Y] = value; }
        }

        public int Size
        {
            get { return r_Size; }
        }

        public ePlayers[,] Board
        {
            get { return m_Board; }
        }

        public GameBoard CloneBoard()
        {
            GameBoard cloned = new GameBoard(Size);

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
