namespace Othello
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Program
    {
        private static GameBoard board;
        private static Player player1, player2;

        public static void Main()
        {
            startNewGame();
        }

        private static void startNewGame()
        {
            eGameType gameType = chooseGameType();
            int boardSize = DataFromConsole.getBoardSize();

            initParams(gameType, boardSize);
            string numOfPlayersStr = (gameType == eGameType.OnePlayer) ? "One player" : "Two players";
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(string.Format("{0} game was chosen, Press Enter to begin", numOfPlayersStr));
            Console.ReadLine();
            Ex02.ConsoleUtils.Screen.Clear();
            startGame(gameType);
        }

        private static void initParams(eGameType gameType, int i_boardSize)
        {
            board = new GameBoard(i_boardSize);
            player1 = new Player(DataFromConsole.getPlayerName("First"));
            if (gameType == eGameType.OnePlayer)
            {
                player2 = new Player("Computer");
            }
            else
            {
                player2 = new Player(DataFromConsole.getPlayerName("Second"));
            }
        }

        private static void startGame(eGameType gameType)
        {
            View.DrawBoard(board);
            Console.ReadLine();
        }

        public enum eGameType
        {
            OnePlayer = 1,
            TwoPlayers = 2
        }

        private static eGameType chooseGameType()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            while (true)
            {
                Console.WriteLine(@"To Start a new Othello game please choose a game type and press Enter:
1) one player game 
2) two players game.");
                string gameType = Console.ReadLine();

                if (gameType == ((int)eGameType.OnePlayer).ToString())
                {
                    return eGameType.OnePlayer;
                }
                else if (gameType == ((int)eGameType.TwoPlayers).ToString())
                {
                    return eGameType.TwoPlayers;
                }

                Console.WriteLine("Invalid input! Please try again:");
            }
        }
    }
}
