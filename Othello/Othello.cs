﻿namespace Othello
{
    using System;

    public class Othello
    {
        private const string k_ExitGame = "Q";
        private const int k_AutoPlayerRecDepth = 2;
        private Player m_Player1, m_Player2;
        private GameBoard m_Board;

        public bool StartNewGame()
        {
            eGameType gameType = ConsoleHandler.ChooseGameType();
            int boardSize = ConsoleHandler.GetBoardSize();
            m_Board = new GameBoard(boardSize);

            setPlayers(gameType);
            bool keepPlay = !startPlay(gameType, m_Board);

            return keepPlay;
        }

        private bool startPlay(eGameType i_GameType, GameBoard i_Board)
        {
            bool exitGame = false;
            bool canPlayerOnePlay = m_Player1.GetValidateMoves(i_Board).Count > 0;

            while (true)
            {
                if (canPlayerOnePlay)
                {
                    exitGame = playTurn(m_Player1);
                }

                bool canPlayerTwoPlay = m_Player2.GetValidateMoves(i_Board).Count > 0;

                if((!canPlayerOnePlay && !canPlayerTwoPlay) || exitGame)
                {
                    break;
                }
                
                if (!canPlayerOnePlay)
                {
                    ConsoleHandler.noMovesMessage(m_Player1, m_Board);
                }
                
                if (canPlayerTwoPlay)
                {
                    if (i_GameType == eGameType.TwoPlayers)
                    {
                        exitGame = playTurn(m_Player2);
                    }
                    else if (i_GameType == eGameType.OnePlayer)
                    {
                        View.DrawBoard(m_Board);
                        Console.WriteLine("Computer is Playing, Please wait for your turn.");
                        AutoPlay.SmartPlay(m_Player2, m_Player1, m_Board, k_AutoPlayerRecDepth);
                    }
                }

                canPlayerOnePlay = m_Player1.GetValidateMoves(i_Board).Count > 0;

                if((!canPlayerOnePlay && !canPlayerTwoPlay) || exitGame)
                {
                    break;
                }

                if (!canPlayerTwoPlay)
                {
                    ConsoleHandler.noMovesMessage(m_Player2, m_Board);
                }
            }

            if (!exitGame)
            {
                View.ShowScore(m_Player1, m_Player2, m_Board);
            }

            return exitGame;
        }

        private bool playTurn(Player i_Player)
        {
            string msg = string.Format("{0}, Please choose a cell and press Enter:", i_Player.Name);
            bool exitGame = false;

            while (true)
            {
                View.DrawBoard(m_Board);
                Console.WriteLine(msg);
                string playedCellStr = Console.ReadLine();

                if (playedCellStr.ToUpper() == k_ExitGame)
                {
                    exitGame = true;
                    break;
                }

                if (Controller.TryPlayMove(i_Player, playedCellStr, ref msg, m_Board))
                {
                    break;
                }
            }

            return exitGame;
        }

        private void setPlayers(eGameType gameType)
        {
            string playerName = ConsoleHandler.GetPlayerName("First");
            m_Player1 = new Player(playerName, ePlayers.Player1, m_Board);
            if (gameType == eGameType.OnePlayer)
            {
                m_Player2 = new Player("Computer", ePlayers.Player2, m_Board);
            }
            else
            {
                playerName = ConsoleHandler.GetPlayerName("Second");
                m_Player2 = new Player(playerName, ePlayers.Player2, m_Board);
            }
        }
    }
}
