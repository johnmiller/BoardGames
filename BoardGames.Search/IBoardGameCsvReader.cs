using System.Collections.Generic;

namespace BoardGames.Search
{
    public interface IBoardGameCsvReader
    {
        IEnumerable<BoardGame> Read();
    }
}