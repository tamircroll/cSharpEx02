using System;

namespace Othello
{
    public class DataFromConsole
    {
        internal static string getPlayerName(string i_playerType)
        {
            string playerName;

            Ex02.ConsoleUtils.Screen.Clear();
            while (true)
            {
                Console.WriteLine(string.Format("Please enter {0} player name and press Enter:", i_playerType));
                playerName = Console.ReadLine();
                if (playerName.Length > 0)
                {
                    return playerName;
                }

                Ex02.ConsoleUtils.Screen.Clear();
                Console.WriteLine("Invalid input!");
            }
        }

        internal static int getBoardSize()
        {
            int boardSizeInt;
            string boardSizeStr;

            Ex02.ConsoleUtils.Screen.Clear();
            while (true)
            {
                Console.WriteLine("Please enter the wanted board size and press Enter");
                boardSizeStr = Console.ReadLine();
                bool goodInput = int.TryParse(boardSizeStr, out boardSizeInt);
                if (goodInput && boardSizeInt >= 8)
                {
                    return boardSizeInt;
                }

                Ex02.ConsoleUtils.Screen.Clear();
                Console.WriteLine("Invalid input! Please try again:");
            }
        }
    }
}