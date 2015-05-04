namespace Othello
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Player
    {
        private readonly string r_Name;
        private readonly ePlayers r_PlayerEnum;

        public string Name 
        {
            get { return r_Name; }
        }

        public ePlayers PlayerEnum
        {
            get { return r_PlayerEnum; }
        }

        public Player(string i_Name, ePlayers i_PlayerEnum)
        {
            r_Name = i_Name;
            r_PlayerEnum = i_PlayerEnum;
        }
    }
}
