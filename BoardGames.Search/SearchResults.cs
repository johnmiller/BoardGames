using System.Collections.Generic;

namespace BoardGames.Search
{
    public class SearchResults
    {
        public SearchCriteria Criteria { get; set; }
        public long TotalMatches { get; set; }
        public IEnumerable<BoardGame> Items { get; set; }
        public IEnumerable<FacetItem> GameTypeFilters { get; set; }
        public IEnumerable<FacetItem> PlayingTimeFilters { get; set; }
        public IEnumerable<FacetItem> AgeFilters { get; set; }

        public SearchResults()
        {
            Criteria = new SearchCriteria();
        }
    }
}