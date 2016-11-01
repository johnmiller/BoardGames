using System.Collections.Generic;
using System.IO;
using CsvHelper;

namespace BoardGames.Search
{
    public class BoardGameCsvReader : IBoardGameCsvReader
    {
        private readonly IBoardGameCsvRowMapper _rowMapper;

        public BoardGameCsvReader(IBoardGameCsvRowMapper rowMapper)
        {
            _rowMapper = rowMapper;
        }

        public IEnumerable<BoardGame> Read()
        {
            using (var reader = File.OpenText(@"../boardgames-sample.csv"))
            using (var csv = new CsvReader(reader))
            {
                while (csv.Read())
                    yield return _rowMapper.Map(csv);
            }
        }
    }
}