namespace Othello
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Program
    {
        private const string k_ExitGame = "Q";
        private static Player s_Player1, s_Player2;
        private static Controller s_Controller;
        private static GameBoard s_Board;

        public static void Main()
        {
            startNewGame();
        }

        private static void startNewGame()
        {
            bool exitGame = false;

            while (!exitGame)
            {
                eGameType gameType = DataFromConsoleHandler.ChooseGameType();
                int boardSize = DataFromConsoleHandler.GetBoardSize();

                initParams(gameType, boardSize);
                Ex02.ConsoleUtils.Screen.Clear();
                exitGame = startPlay(gameType);
            }
        }

        private static void initParams(eGameType gameType, int i_boardSize)
        {
            s_Board = new GameBoard(i_boardSize);
            s_Controller = new Controller(s_Board);
            string playerName = DataFromConsoleHandler.GetPlayerName("First");
            s_Player1 = new Player(playerName, ePlayers.Player1);
            
            if (gameType == eGameType.OnePlayer)
            {
                s_Player2 = new Player("Computer", ePlayers.Player2);
            }
            else
            {
                playerName = DataFromConsoleHandler.GetPlayerName("Second");
                s_Player2 = new Player(playerName, ePlayers.Player2);
            }
        }

        private static bool startPlay(eGameType i_GameType)
        {
            bool isGameOver = false, exitGame = false;
            bool canPlayerTwoPlay = s_Controller.ListAllPossibleMoves(s_Player2);
            bool canPlayerOnePlay = s_Controller.ListAllPossibleMoves(s_Player1);

            while (true)
            {
                if (canPlayerOnePlay)
                {
                    exitGame = playTurn(s_Player1);
                }

                canPlayerTwoPlay = s_Controller.ListAllPossibleMoves(s_Player2);
                if((!canPlayerOnePlay && !canPlayerTwoPlay) || exitGame)
                {
                    break;
                }
                
                if (!canPlayerOnePlay)
                {
                    noMovesMessage(s_Player1);
                }
                
                if (canPlayerTwoPlay)
                {
                    if (i_GameType == eGameType.TwoPlayers)
                    {
                        exitGame = playTurn(s_Player2);
                    }
                    else if (i_GameType == eGameType.OnePlayer)
                    {
                        AutoPlay.Play(s_Player2, s_Player1, s_Board, s_Controller);
                    }
                }

                canPlayerOnePlay = s_Controller.ListAllPossibleMoves(s_Player1);
                if((!canPlayerOnePlay && !canPlayerTwoPlay) || exitGame)
                {
                    break;
                }

                if (!canPlayerTwoPlay)
                {
                    noMovesMessage(s_Player2);
                }
            }

            if (!exitGame)
            {
                s_Controller.CalcAndShowScore(s_Player1, s_Player2);
            }

            return exitGame;
        }

        private static void noMovesMessage(Player i_Player)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            View.DrawBoard(s_Board);
            Console.WriteLine(string.Format("{0}, You don't have any possible move, Press Enter to pass the turn.", i_Player.Name));
            Console.ReadLine();
        }

        private static bool playTurn(Player player)
        {
            string msg = string.Format("{0}, please write your play and press Enter:", player.Name);
            bool exitGame = false;

            while (true)
            {
                View.DrawBoard(s_Board);
                Console.WriteLine(msg);
                string playedCell = Console.ReadLine();
                if (playedCell == k_ExitGame)
                {
                    exitGame = true;
                    break;
                }

                if (s_Controller.TryPlayMove(player, playedCell, ref msg))
                {
                    break;
                }
            }

            return exitGame;
        }

        [Flags]
        public enum eGameType
        {
            OnePlayer = 1,
            TwoPlayers = 2
        }
    }
}
