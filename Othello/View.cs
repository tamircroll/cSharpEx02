namespace Othello
{
    using System;

    public class View
    {
        public static void DrawBoard(GameBoard i_GameBoard)
        {
            int boardSize = i_GameBoard.Size;
            ePlayers[,] board = i_GameBoard.Board;
            char column = 'A';
            
            Ex02.ConsoleUtils.Screen.Clear();
            Console.Write(" ");
            for (int i = 0; i < boardSize; i++)
            {
                Console.Write("   " + (column++));
            }

            Console.WriteLine("  ");
            for (int j = 0; j < boardSize; j++)
            {
                Console.Write("  ");
                for (int i = 0; i < (boardSize * 4) + 1; i++)
                {
                    Console.Write("=");
                }

                Console.WriteLine();
                if ((j + 1) <= 9)
                {
                    Console.Write(" ");
                }

                Console.Write(j + 1);
                for (int i = 0; i < boardSize; i++)
                {
                    Console.Write(string.Format("| {0} ", getCellSign(board[j, i])));
                }

                Console.WriteLine("|");
            }

            Console.Write("  ");
            for (int i = 0; i < (boardSize * 4) + 1; i++)
            {
                Console.Write("=");
            }

            Console.WriteLine();
        }

        private static string getCellSign(ePlayers i_PlayerEnum)
        {
            switch (i_PlayerEnum)
            {
                case ePlayers.NoPlayer:
                    return " ";
                case ePlayers.Player1:
                    return "X";
                case ePlayers.Player2:
                    return "O";
            }

            throw new Exception("Couldn't find a cell sign");
        }

        internal static void ShowScore(Player i_FirstPlayer, Player i_SecondPlayer, GameBoard i_Board)
        {
            string winnerMsg = @"{0} IS THE WINNER!!!

                                   .''.
       .''.      .        *''*    :_\/_:     .
      :_\/_:   _\(/_  .:.*_\/_*   : /\ :  .'.:.'.
  .''.: /\ :    /)\   ':'* /\ *  : '..'.  -=:o:=-
 :_\/_:'.:::.  | ' *''*    * '.\'/.'_\(/_ '.':'.'
 : /\ : :::::  =  *_\/_*     -= o =- /)\    '  *
  '..'  ':::' === * /\ *     .'/.\'.  ' ._____
      *        |   *..*         :       |.   |' .----|
        *      |     _           .--'|  ||   | _|    |
        *      |  .-'|       __  |   |  |    ||      |
     .-----.   |  |' |  ||  |  | |   |  |    ||      |
 ___'       ' / \ |  '-.''.    '-'   '-.'    '`      |____
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         Press Enter to start a new game";

            string tieMsg = @"YOU BOTH WINNERS!!!

                ,a_a
               {/ ''\_
               {\ ,_oo)
               {/  (_^_____________________
     .=.      {/ \___)))*)----------;=====;`
    (.=.`\   {/   /=;  ~~           |     |
        \ `\{/(   \/\               |     |
         \  `. `\  ) )              |     |
          \    // /_/_              |     |
           '==''---))))             |_____|
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         Press Enter to start a new game";

            Ex02.ConsoleUtils.Screen.Clear();
            View.DrawBoard(i_Board);
            Console.WriteLine(string.Format(
@" _______  _______  _______  _______    _______           _______  _______ 
(  ____ \(  ___  )(       )(  ____ \  (  ___  )|\     /|(  ____ \(  ____ )
| (    \/| (   ) || () () || (    \/  | (   ) || )   ( || (    \/| (    )|
| |      | (___) || || || || (__      | |   | || |   | || (__    | (____)|
| | ____ |  ___  || |(_)| ||  __)     | |   | |( (   ) )|  __)   |     __)
| | \_  )| (   ) || |   | || (        | |   | | \ \_/ / | (      | (\ (   
| (___) || )   ( || )   ( || (____/\  | (___) |  \   /  | (____/\| ) \ \__
(_______)|/     \||/     \|(_______/  (_______)   \_/   (_______/|/   \__/

{0} have {1} points, {2} have {3} points.",
                    i_FirstPlayer.Name,
                    i_FirstPlayer.Score,
                    i_SecondPlayer.Name,
                    i_SecondPlayer.Score));

            if (i_FirstPlayer.Score > i_SecondPlayer.Score)
            {
                Console.WriteLine(string.Format(winnerMsg, i_FirstPlayer.Name));
                Console.ReadLine();
            }
            else if (i_SecondPlayer.Score > i_FirstPlayer.Score)
            {
                Console.WriteLine(string.Format(winnerMsg, i_SecondPlayer.Name));
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine(tieMsg);
            }
        }
    }
}
