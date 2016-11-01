using System.Collections.Generic;
using CsvHelper;

namespace BoardGames.Search
{
    public class BoardGameCsvRowMapper : IBoardGameCsvRowMapper
    {
        private static Dictionary<string, GameType> _gameTypeMappings = new Dictionary<string, GameType>
        {
            {"boardgame", GameType.BoardGame},
            {"boardgameexpansion", GameType.BoardGameExpansion}
        };

        public BoardGame Map(ICsvReaderRow row)
        {
            return new BoardGame
            {
                Id = row.GetField<long>("id"),
                GameType = MapGameType(row.GetField<string>("type")),
                Name = row.GetField<string>("name"),
                YearPublished = row.GetField<int?>("yearpublished"),
                MinPlayers = row.GetField<int?>("minplayers"),
                MaxPlayers = row.GetField<int?>("maxplayers"),
                PlayingTime = row.GetField<int?>("playingtime"),
                MinAge = row.GetField<int?>("minage"),
                AverageRating = row.GetField<decimal?>("average_rating"),
                TotalOwners = row.GetField<int?>("total_owners")
            };
        }

        private static GameType MapGameType(string gameType)
        {
            return _gameTypeMappings.GetValueOrDefault(gameType);
        }
    }
}