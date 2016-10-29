using System;
using System.IO;
using CsvHelper;
using Nest;

namespace BoardGames.Search
{
    public class SearchIndexBuilder : ISearchIndexBuilder
    {
        private const string IndexName = "games";

        public void RebuildIndexes()
        {
            var client = CreateClient();
            BuildIndexes(client);
            PopulateGames(client);
        }

        private IElasticClient CreateClient()
        {
            var node = new Uri("http://localhost:9201");
            var settings = new Nest.ConnectionSettings(node);

            return new ElasticClient(settings);
        }

        private void BuildIndexes(IElasticClient client)
        {
            if (client.IndexExists(IndexName).Exists)
                client.DeleteIndex(IndexName);

            client.CreateIndex(IndexName, indexDescriptor => indexDescriptor
                .Settings(s => s
                    .NumberOfShards(1)
                    .NumberOfReplicas(0))
                .Mappings(mappings => mappings
                    .Map<BoardGame>(mapper => mapper.AutoMap())));

            client.Map<BoardGame>(mapper => mapper.Index(IndexName).AutoMap());
        }

        private void PopulateGames(IElasticClient client)
        {
            using (var reader = File.OpenText(@"../boardgames-sample.csv"))
            using (var csv = new CsvReader(reader))
            {

                while (csv.Read())
                    client.Index(Map(csv), idx => idx.Index(IndexName));
            }

        }

        private BoardGame Map(CsvReader csv)
        {
            return new BoardGame
            {
                Id = csv.GetField<long>("id"),
                GameType = csv.GetField<string>("type"),
                Name = csv.GetField<string>("name"),
                YearPublished = csv.GetField<int?>("yearpublished"),
                MinPlayers = csv.GetField<int?>("minplayers"),
                MaxPlayers = csv.GetField<int?>("maxplayers"),
                PlayingTime = csv.GetField<int?>("playingtime"),
                MinAge = csv.GetField<int?>("minage"),
                AverageRating = csv.GetField<decimal?>("average_rating"),
                TotalOwners = csv.GetField<int?>("total_owners")
            };
        }
    }
}