using System.Globalization;

namespace Othello
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class View
    {
        public static void DrawBoard(GameBoard i_GameBoard)
        {
            int boardSize = i_GameBoard.Size;
            ePlayers[,] board = i_GameBoard.Board;
            char column = 'A';
            
            Ex02.ConsoleUtils.Screen.Clear();
            Console.Write(" ");
            for (int i = 0; i < boardSize; i++)
            {
                Console.Write("   " + (column++));
            }

            Console.WriteLine("  ");
            for (int j = 0; j < boardSize; j++)
            {
                Console.Write("  ");
                for (int i = 0; i < (boardSize * 4) + 1; i++)
                {
                    Console.Write("=");
                }

                Console.WriteLine();
                if ((j + 1) <= 9)
                {
                    Console.Write(" ");
                }

                Console.Write(j + 1);
                for (int i = 0; i < boardSize; i++)
                {
                    Console.Write(string.Format("| {0} ", getCellSign(board[j, i])));
                }

                Console.WriteLine("|");
            }

            Console.Write("  ");
            for (int i = 0; i < (boardSize * 4) + 1; i++)
            {
                Console.Write("=");
            }

            Console.WriteLine();
        }

        private static string getCellSign(ePlayers ePlayers)
        {
            switch (ePlayers)
            {
                case ePlayers.NoPlayer:
                    return " ";
                case ePlayers.Player1:
                    return "X";
                case ePlayers.Player2:
                    return "O";
            }

            throw new Exception("Couldn't find a cell sign");
        }

        internal static void ShowScore(Player i_FirstPlayer, Player i_SecondPlayer, Player i_Winner)
        {
            Console.WriteLine(string.Format(
@"   GAME OVER!!!
{0}: {1}, {2}: {3}.",
                    i_FirstPlayer.Name,
                    i_FirstPlayer.m_Score,
                    i_SecondPlayer.Name,
                    i_SecondPlayer.m_Score));

            if (i_Winner != null)
            {
                Console.WriteLine(string.Format("{0} IS THE WINNER!!!", i_Winner.Name));
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("YOU BOTH WINNERS!!!");
            }
        }
    }
}
