using System.Reflection.Emit;

namespace Othello
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class GameBoard
    {
        private readonly int _size;

        public int Size
        {
            get
            {
                return _size;
            } 
        }

        public ePlayers[,] Board
        {
            get; set;
        }

        public GameBoard(int i_Size)
        {
            _size = i_Size;
            Board = new ePlayers[i_Size, i_Size];
            Board[i_Size / 2, i_Size / 2] = ePlayers.Player1;
            Board[(i_Size / 2) - 1, (i_Size / 2) - 1] = ePlayers.Player1;
            Board[i_Size / 2, (i_Size / 2) - 1] = ePlayers.Player2;
            Board[(i_Size / 2) - 1, i_Size / 2] = ePlayers.Player2;
        }
    }
}
