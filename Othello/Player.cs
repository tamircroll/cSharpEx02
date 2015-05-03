namespace Othello
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Player
    {
        public readonly string Name;

        public Player(string i_Name)
        {
            Name = i_Name;
        }

        public string getName()
        {
            return Name;
        }
    }
}
