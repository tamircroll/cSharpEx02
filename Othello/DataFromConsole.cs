using System;

namespace Othello
{
    internal class DataFromConsole
    {
        internal static string GetPlayerName(string i_playerType)
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

        internal static int GetBoardSize()
        {
            int boardSizeInt;
            string boardSizeStr;

            Ex02.ConsoleUtils.Screen.Clear();
            while (true)
            {
                Console.WriteLine("Please enter the wanted board size and press Enter");
                boardSizeStr = Console.ReadLine();
                bool goodInput = int.TryParse(boardSizeStr, out boardSizeInt);
                if (goodInput && boardSizeInt >= 8 && boardSizeInt < 26)
                {
                    return boardSizeInt;
                }

                Ex02.ConsoleUtils.Screen.Clear();
                Console.WriteLine("Invalid input! Please try again:");
            }
        }

        public static Program.eGameType ChooseGameType()
        {
            while (true)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                Console.WriteLine(
@"To Start a new Othello game please choose a game type and press Enter:
1) one player game 
2) two players game.");
                string gameType = Console.ReadLine();

                if (gameType == ((int)Program.eGameType.OnePlayer).ToString())
                {
                    return Program.eGameType.OnePlayer;
                }
                else if (gameType == ((int)Program.eGameType.TwoPlayers).ToString())
                {
                    return Program.eGameType.TwoPlayers;
                }

                Console.WriteLine("Invalid input! Please try again:");
            }
        }
    }
}