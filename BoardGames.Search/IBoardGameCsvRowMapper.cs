using CsvHelper;

namespace BoardGames.Search
{
    public interface IBoardGameCsvRowMapper
    {
        BoardGame Map(ICsvReaderRow row);
    }
}