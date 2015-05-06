using System;
using Ex02.ConsoleUtils;

namespace Othello
{
    internal class DataFromConsoleHandler
    {
        public const string k_BoardSizeSix = "1";
        public const string k_BoardSizeEight = "2";

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
            string option;

            Ex02.ConsoleUtils.Screen.Clear();
            while (true)
            {
                Console.WriteLine(string.Format(
@"Please choose board size:
{0}) Board size 6
{1}) Board size 8",
                  k_BoardSizeSix, 
                  k_BoardSizeEight));
                option = Console.ReadLine();
                if (option == k_BoardSizeSix)
                {
                    boardSizeInt = 6;
                    break;
                }
                else if (option == k_BoardSizeEight)
                {
                    boardSizeInt = 8;
                    break;
                }

                Ex02.ConsoleUtils.Screen.Clear();
                Console.WriteLine("Invalid input! Please try again:");
            }

            return boardSizeInt;
        }

        public static Program.eGameType ChooseGameType()
        {
            while (true)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                Console.WriteLine(string.Format(
@"To Start a new Othello game please choose a game type and press Enter:
{0}) one player game 
{1}) two players game.", 
                       (int)Program.eGameType.OnePlayer, 
                       (int)Program.eGameType.TwoPlayers));
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