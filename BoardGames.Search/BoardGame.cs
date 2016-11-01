using System;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace BoardGames.Search
{
    [ElasticsearchType(Name = "boardgame")]
    public class BoardGame
    {
        public long Id { get; set; }

        public int GameTypeId
        {
            get { return GameType.Id; }
            set{ GameType = GameType.Find(value); }
        }

        [Object(Ignore = true)]
        public GameType GameType { get; set; }

        public string Name { get; set; }

        public int? YearPublished { get; set; }

        public int? MinPlayers { get; set; }

        public int? MaxPlayers { get; set; }

        public int? PlayingTime { get; set; }

        public int? MinAge { get; set; }

        public decimal? AverageRating { get; set; }

        public int? TotalOwners { get; set; }
    }
}


