namespace Othello
{
    using System.Collections.Generic;

    public class Player
    {
        private readonly string r_Name;
        private readonly ePlayers r_PlayerEnum;

        public Player(string i_Name, ePlayers i_PlayerEnum)
        {
            r_Name = i_Name;
            r_PlayerEnum = i_PlayerEnum;
        }

        public int m_Score
        {
            get; set;
        }

        public List<string> ValidateMoves
        {
            get; set;
        }

        public string Name 
        {
            get
            {
                return r_Name;
            }
        }

        public ePlayers PlayerEnum
        {
            get
            {
                return r_PlayerEnum;
            }
        }

        public Player ClonePlayer()
        {
            Player cloned = new Player(Name, PlayerEnum);
            cloned.ValidateMoves = new List<string>();

            foreach (string move in ValidateMoves)
            {
                cloned.ValidateMoves.Add(move);
            }

            return cloned;
        }
    }
}
