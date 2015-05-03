using System.Reflection.Emit;

namespace Othello
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class GameBoard
    {
        private readonly int r_Size;

        public int Size
        {
            get
            {
                return r_Size;
            } 
        }

        public ePlayers[,] Board
        {
            get; set;
        }

        public GameBoard(int iSize)
        {
            r_Size = iSize;
            Board = new ePlayers[iSize, iSize];
            Board[iSize / 2, iSize / 2] = ePlayers.Player1;
            Board[(iSize / 2) - 1, (iSize / 2) - 1] = ePlayers.Player1;
            Board[iSize / 2, (iSize / 2) - 1] = ePlayers.Player2;
            Board[(iSize / 2) - 1, iSize / 2] = ePlayers.Player2;
        }
    }
}
