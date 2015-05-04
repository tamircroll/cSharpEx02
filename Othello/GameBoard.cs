using System.Reflection.Emit;

namespace Othello
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class GameBoard
    {
        private readonly int r_Size;

        public GameBoard(int i_Size)
        {
            r_Size = i_Size;
            Board = new ePlayers[i_Size, i_Size];
            Board[i_Size / 2, i_Size / 2] = ePlayers.Player1;
            Board[(i_Size / 2) - 1, (i_Size / 2) - 1] = ePlayers.Player1;
            Board[i_Size / 2, (i_Size / 2) - 1] = ePlayers.Player2;
            Board[(i_Size / 2) - 1, i_Size / 2] = ePlayers.Player2;
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
            get;
        }
    }
}
