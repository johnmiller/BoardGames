using System.Collections.Generic;
using System.Linq;
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
                TotalMatches = boardGameResults.Total,
                GameTypeFilters = FilterList(boardGameResults, "game_type"),
                PlayingTimeFilters = FilterList(boardGameResults, "playing_time"),
                AgeFilters = FilterList(boardGameResults, "age")
            };
        }

        private IEnumerable<FacetItem> FilterList(ISearchResponse<BoardGame> boardGameResults, string term)
        {
            return boardGameResults.Aggs
                .Terms(term)
                .Buckets
                .Select(x => new FacetItem{Description = x.Key, Count = x.DocCount ?? 0});
        }
    }
}