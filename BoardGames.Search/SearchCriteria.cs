using System.Collections.Generic;

namespace BoardGames.Search
{
    public class SearchCriteria
    {
        public string SearchText { get; set; }
        public IEnumerable<string> SelectedGameTypes { get; set; }

        public SearchCriteria()
        {
            SelectedGameTypes = new List<string>();
        }
    }
}