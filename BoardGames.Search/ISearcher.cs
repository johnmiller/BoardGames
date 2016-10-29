namespace BoardGames.Search
{
    public interface ISearcher
    {
        SearchResults Search(SearchCriteria criteria);
    }
}