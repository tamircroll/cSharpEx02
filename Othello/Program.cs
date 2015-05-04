namespace Othello
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Program
    {
        private const string k_ExitGame = "Q";
        private static GameBoard s_Board;
        private static Player s_Player1, s_Player2;
        private static Controller s_Controller;

        public static void Main()
        {
            startNewGame();
        }

        private static void startNewGame()
        {
            eGameType gameType = DataFromConsole.ChooseGameType();
            int boardSize = DataFromConsole.GetBoardSize();

            initParams(gameType, boardSize);
            string numOfPlayersStr = (gameType == eGameType.OnePlayer) ? "One player" : "Two players";
            Ex02.ConsoleUtils.Screen.Clear();
            string msg = string.Format("{0} game was chosen, Press Enter to begin", numOfPlayersStr);
            Console.WriteLine(msg);
            Console.ReadLine();
            Ex02.ConsoleUtils.Screen.Clear();
            startPlay(gameType);
        }

        private static void initParams(eGameType gameType, int i_boardSize)
        {
            s_Board = new GameBoard(i_boardSize);
            s_Controller = new Controller(s_Board);
            string playerName = DataFromConsole.GetPlayerName("First");
            s_Player1 = new Player(playerName, ePlayers.Player1);
            if (gameType == eGameType.OnePlayer)
            {
                s_Player2 = new Player("Computer", ePlayers.Player2);
            }
            else
            {
                playerName = DataFromConsole.GetPlayerName("Second");
                s_Player2 = new Player(playerName, ePlayers.Player2);
            }
        }

        private static void startPlay(eGameType i_GameType)
        {
            while (!s_Controller.IsGameOver())
            {
                string badMsg = "";
                while (true)
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    View.DrawBoard(s_Board);
                    Console.WriteLine(badMsg);
                    Console.WriteLine(String.Format("{0}, please write your play and press Enter:", s_Player1.Name));
                    string playedCell = Console.ReadLine();
                    if (playedCell == k_ExitGame)
                    {
                        return;
                    }

                    if (s_Controller.TryPlayMove(s_Player1, playedCell, ref badMsg))
                    {
                        break;
                    }
                }

                if (s_Controller.IsGameOver())
                {
                    s_Controller.ShowScore();
                    break;
                }

                badMsg = "";
                while (true)
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    View.DrawBoard(s_Board);
                    Console.WriteLine(badMsg);
                    Console.WriteLine("Player TWO turn, please enter the desired cell:");
                    string playedCell = Console.ReadLine();
                    if (playedCell == k_ExitGame)
                    {
                        return;
                    }
 
                    if (s_Controller.TryPlayMove(s_Player2, playedCell, ref badMsg))
                    {
                        break;
                    }
                    Console.WriteLine(badMsg);
                }
            }
            startNewGame();
        }

        public enum eGameType
        {
            OnePlayer = 1,
            TwoPlayers = 2
        }
    }
}
