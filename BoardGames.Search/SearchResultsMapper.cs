using Nest;

namespace BoardGames.Search
{
    public class SearchResultsMapper : ISearchResultsMapper
    {
        public SearchResults Map(SearchCriteria criteria, ISearchResponse<BoardGame> boardGameResults)
        {
            return new SearchResults
            {
                Criteria = criteria,
                Items = boardGameResults.Documents,
                TotalMatches = boardGameResults.Total
            };
        }
    }
}