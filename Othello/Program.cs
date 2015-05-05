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
            bool exitGame = false;

            while (!exitGame)
            {
                eGameType gameType = DataFromConsole.ChooseGameType();
                int boardSize = DataFromConsole.GetBoardSize();

                initParams(gameType, boardSize);
                Ex02.ConsoleUtils.Screen.Clear();
                exitGame = startPlay(gameType);
            }
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

        private static bool startPlay(eGameType i_GameType)
        {
            bool isGameOver = false, exitGame = false, canPlayerOnePlay, canPlayerTwoPlay = true;
            while (!isGameOver)
            {
                canPlayerOnePlay = s_Controller.CanPlayerPlay(s_Player1);
                if (canPlayerOnePlay)
                {
                    exitGame = playTurn(s_Player1);
                }

                if (exitGame)
                {
                    break;
                }

                canPlayerTwoPlay = s_Controller.CanPlayerPlay(s_Player2);
                if (canPlayerTwoPlay)
                {
                    exitGame = playTurn(s_Player2);
                }

                if (exitGame)
                {
                    break;
                }
                
                isGameOver = (!canPlayerOnePlay && !canPlayerTwoPlay) || exitGame;
            }
            if (!exitGame)
            {
                s_Controller.CalcAndShowScore();
            }

            return exitGame;
        }

        private static bool playTurn(Player player)
        {
            string badMsg = string.Empty;
            bool exitGame = false;

            while (true)
            {
                View.DrawBoard(s_Board);
                Console.WriteLine(badMsg);
                Console.WriteLine(string.Format("{0}, please write your play and press Enter:", player.Name));
                string playedCell = Console.ReadLine();
                if (playedCell == k_ExitGame)
                {
                    exitGame = true;
                    break;
                }

                if (s_Controller.TryPlayMove(player, playedCell, ref badMsg))
                {
                    break;
                }
            }

            return exitGame;
        }

        public enum eGameType
        {
            OnePlayer = 1,
            TwoPlayers = 2
        }
    }
}
