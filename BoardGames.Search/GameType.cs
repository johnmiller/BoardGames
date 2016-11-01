using System.Collections.Generic;
using System.Linq;

namespace BoardGames.Search
{
    public class GameType
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public static List<GameType> All = new List<GameType>();

        public GameType(int id, string description)
        {
            Id = id;
            Description = description;
            All.Add(this);
        }

        public static GameType Find(int id)
        {
            return All.FirstOrDefault(x => x.Id == id);
        }

        public static GameType BoardGame = new GameType(1, "Boardgame");
        public static GameType BoardGameExpansion = new GameType(2, "Boardgame Expansion");
    }
}