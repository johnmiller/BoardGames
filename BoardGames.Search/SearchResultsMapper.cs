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
                AgeFilters = FilterList(boardGameResults, "age"),
                PlayingTimeFilters = FilterList(boardGameResults, "playing_time")
            };
        }

        private IEnumerable<FacetItem> FilterList(ISearchResponse<BoardGame> boardGameResults, string term)
        {
            return boardGameResults.Aggs
                .Terms(term)
                .Buckets
                .Select(x => new FacetItem{Description = x.Key.ToString(), Count = x.DocCount ?? 0});
        }
    }
}