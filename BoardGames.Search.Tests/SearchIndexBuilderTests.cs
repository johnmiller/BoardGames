using NSubstitute;
using Nest;
using Xunit;

namespace BoardGames.Search.Tests
{
    public class SearchIndexBuilderTests
    {
        private readonly IElasticClient _mockClient;
        private readonly IBoardGameCsvReader _mockCsvReader;
        private SearchIndexBuilder _searchIndexBuilder;


        public SearchIndexBuilderTests()
        {
            _mockClient = Substitute.For<IElasticClient>();
            _mockCsvReader = Substitute.For<IBoardGameCsvReader>();
            _searchIndexBuilder = new SearchIndexBuilder(_mockClient, _mockCsvReader);
        }

        [Fact]
        public void RebuildIndexes_DeletesExistingIndexIfExists()
        {
            MockIndexExistsCall(true);

            _searchIndexBuilder.RebuildIndexes();

            _mockClient.Received().DeleteIndex("games");
        }

        [Fact]
        public void RebuildIndexes_SkipsDeletingIndexIfNonExisting()
        {
            MockIndexExistsCall(false);

            _searchIndexBuilder.RebuildIndexes();

            _mockClient.DidNotReceive().DeleteIndex("games");
        }

        private void MockIndexExistsCall(bool indexExists)
        {
            var mockResponse = Substitute.For<IExistsResponse>();
            mockResponse.Exists.Returns(indexExists);
            _mockClient.IndexExists("games").Returns(mockResponse);
        }
    }
}


