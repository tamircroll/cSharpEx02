namespace Othello
{
    public class Program
    {
        public static void Main()
        {
            Othello othello;
            bool keepPlayng;

            do
            {
                othello = new Othello();
                keepPlayng = othello.StartNewGame();
            }
            while (keepPlayng);
        }
    }
}
