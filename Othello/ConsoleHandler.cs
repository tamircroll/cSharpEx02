using System;

namespace Othello
{
    internal class ConsoleHandler
    {
        internal static string GetPlayerName(string i_playerType)
        {
            string playerName;

            Ex02.ConsoleUtils.Screen.Clear();
            while (true)
            {
                Console.WriteLine(string.Format("Please enter {0} player name and press Enter:", i_playerType));
                playerName = Console.ReadLine();
                if (playerName != string.Empty)
                {
                    break;
                }

                Ex02.ConsoleUtils.Screen.Clear();
                Console.WriteLine("Invalid input!");
            }

            return playerName;
        }

        internal static int GetBoardSize()
        {
            const string k_BoardSizeSix = "1";
            const string k_BoardSizeEight = "2";
            int boardSizeInt;

            Ex02.ConsoleUtils.Screen.Clear();
            while (true)
            {
                Console.WriteLine(string.Format(
@"Please choose board size:
{0}) Board size 6
{1}) Board size 8",
                  k_BoardSizeSix, 
                  k_BoardSizeEight));
                string option = Console.ReadLine();
                if (option == k_BoardSizeSix)
                {
                    boardSizeInt = 6;
                    break;
                }
                
                if (option == k_BoardSizeEight)
                {
                    boardSizeInt = 8;
                    break;
                }

                Ex02.ConsoleUtils.Screen.Clear();
                Console.WriteLine("Do not enter empty input!");
            }

            return boardSizeInt;
        }

        public static eGameType ChooseGameType()
        {
            eGameType gameType;

            Ex02.ConsoleUtils.Screen.Clear();
            while (true)
            {
                Console.WriteLine(string.Format(
@"To Start a new Othello game please choose a game type and press Enter:
{0}) one player game 
{1}) two players game.",
                       (int)eGameType.OnePlayer,
                       (int)eGameType.TwoPlayers));
                string gameTypeStr = Console.ReadLine();

                if (gameTypeStr == ((int)eGameType.OnePlayer).ToString())
                {
                    gameType = eGameType.OnePlayer;
                    break;
                }
                
                if (gameTypeStr == ((int)eGameType.TwoPlayers).ToString())
                {
                    gameType = eGameType.TwoPlayers;
                    break;
                }

                Ex02.ConsoleUtils.Screen.Clear();
                Console.WriteLine("Invalid input! Please try again.");
            }

            return gameType;
        }

        public static void noMovesMessage(Player i_Player, GameBoard i_Board)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            View.DrawBoard(i_Board);
            Console.WriteLine(string.Format("{0}, You don't have any possible move, Press Enter to pass the turn.", i_Player.Name));
            Console.ReadLine();
        }
    }
}