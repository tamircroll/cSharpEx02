namespace Othello
{
    using System;

    public class Othello
    {
        private const string k_ExitGame = "Q";
        private Player m_Player1, m_Player2;
        private Controller m_Controller;
        private GameBoard m_Board;

        public bool StartNewGame()
        {
            eGameType gameType = ConsoleHandler.ChooseGameType();
            int boardSize = ConsoleHandler.GetBoardSize();
            m_Board = new GameBoard(boardSize);
            m_Controller = new Controller(m_Board);

            setPlayers(gameType);
            bool keepPlay = !startPlay(gameType);

            return keepPlay;
        }

        private void setPlayers(eGameType gameType)
        {
            string playerName = ConsoleHandler.GetPlayerName("First");
            m_Player1 = new Player(playerName, ePlayers.Player1);
            if (gameType == eGameType.OnePlayer)
            {
                m_Player2 = new Player("Computer", ePlayers.Player2);
            }
            else
            {
                playerName = ConsoleHandler.GetPlayerName("Second");
                m_Player2 = new Player(playerName, ePlayers.Player2);
            }
        }

        private bool startPlay(eGameType i_GameType)
        {
            bool exitGame = false;
            bool canPlayerOnePlay = m_Controller.ListAllPossibleMoves(m_Player1);

            while (true)
            {
                if (canPlayerOnePlay)
                {
                    exitGame = playTurn(m_Player1);
                }

                bool canPlayerTwoPlay = m_Controller.ListAllPossibleMoves(m_Player2);
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
                        AutoPlay.PlayRandom(m_Player2, m_Controller);
                    }
                }

                canPlayerOnePlay = m_Controller.ListAllPossibleMoves(m_Player1);
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
                m_Controller.CalcScore(m_Player1, m_Player2);
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

                if (m_Controller.TryPlayMove(i_Player, playedCellStr, ref msg))
                {
                    break;
                }
            }

            return exitGame;
        }
    }
}
