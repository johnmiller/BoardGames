using System;
using Nest;

namespace BoardGames.Search
{
    public class SearchIndexBuilder : ISearchIndexBuilder
    {
        private readonly IElasticClient _client;
        private readonly IBoardGameCsvReader _boardGameCsvReader;
        private const string IndexName = "games";

        public SearchIndexBuilder(IElasticClient client, IBoardGameCsvReader boardGameCsvReader)
        {
            _client = client;
            _boardGameCsvReader = boardGameCsvReader;
        }

        public void RebuildIndexes()
        {
            BuildIndexes();
            PopulateGames();
        }

        private void BuildIndexes()
        {
            if (_client.IndexExists(IndexName).Exists)
                _client.DeleteIndex(IndexName);

            _client.CreateIndex(IndexName, indexDescriptor => indexDescriptor
                .Settings(s => s
                    .NumberOfShards(1)
                    .NumberOfReplicas(0))
                .Mappings(mappings => mappings
                    .Map<BoardGame>(mapper => mapper.AutoMap())));

            _client.Map<BoardGame>(mapper => mapper.Index(IndexName).AutoMap());
        }

        private void PopulateGames()
        {
            foreach (var boardgame in _boardGameCsvReader.Read())
                _client.Index(boardgame, idx => idx.Index(IndexName));
        }
    }
}