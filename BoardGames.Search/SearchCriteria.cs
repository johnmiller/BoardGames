using System.Collections.Generic;

namespace BoardGames.Search
{
    public class SearchCriteria
    {
        public string SearchText { get; set; }
        public IEnumerable<int> SelectedGameTypes { get; set; }
        public IEnumerable<int> SelectedAges { get; set; }
        public IEnumerable<int> SelectedPlayingTimes { get; set; }

        public SearchCriteria()
        {
            SelectedGameTypes = new List<int>();
            SelectedAges = new List<int>();
            SelectedPlayingTimes = new List<int>();
        }
    }
}