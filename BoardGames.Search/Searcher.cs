using System;
using System.Collections.Generic;
using System.Linq;
using Nest;

namespace BoardGames.Search
{
    public class Searcher : ISearcher
    {
        private readonly IElasticClient _client;
        private readonly ISearchResultsMapper _searchResultsMapper;

        public Searcher(IElasticClient client, ISearchResultsMapper searchResultsMapper)
        {
            _client = client;
            _searchResultsMapper = searchResultsMapper;
        }

        public SearchResults Search(SearchCriteria criteria)
        {
            var results = _client.Search<BoardGame>(x => x
                    .Aggregations(a => a
                        .Terms("age", ts => ts
                            .Field(f => f.MinAge))
                        .Terms("playing_time", ts => ts
                            .Field(f => f.PlayingTime))
                        .Terms("game_type", ts => ts
                            .Field(f => f.GameType)))
                    .Query(q => q
                        .MultiMatch(m => m
                            .Fields(fields => fields.Field(f => f.Name))
                            .Operator(Operator.And)
                            .Query(criteria.SearchText)))
                    .PostFilter(pf => pf
                        .Terms(t => t
                            .Field(f => f.GameType)
                            .Terms(criteria.SelectedGameTypes)))
                    .From(0)
                    .Take(50));

            return _searchResultsMapper.Map(criteria, results);
        }
    }
}