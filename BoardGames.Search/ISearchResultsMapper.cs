using Nest;

namespace BoardGames.Search
{
    public interface ISearchResultsMapper
    {
        SearchResults Map(SearchCriteria criteria, ISearchResponse<BoardGame> boardGameResults);
    }
}