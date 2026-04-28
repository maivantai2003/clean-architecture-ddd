using Elasticsearch.Net;
using Microsoft.Extensions.Options;
using Nest;
using Search.Application.Abstractions.Persistence;
using Search.Domain.Entities;
using Search.Infrastructure.Config;

namespace Search.Infrastructure.Elasticsearch
{
    public class ElasticUserSearchService : IUserSearchService
    {
        private readonly IElasticClient _elasticClient;
        private readonly string _indexName;
        public ElasticUserSearchService(IElasticClient elasticClient, IOptions<ElasticOptions> options)
        {
            _elasticClient = elasticClient;
            _indexName = options.Value.UserIndex;
        }
        public async Task DeleteAsync(string id)
        {
            await _elasticClient.DeleteAsync<UserDocument>(id, d => d.Index(_indexName));
        }

        public async Task IndexAsync(UserDocument doc)
        {
            var response = await _elasticClient.IndexAsync(doc, i => i.Index(_indexName).Id(doc.UserId));
            if (!response.IsValid)
                throw new Exception(response.DebugInformation);
        }

        public async Task<(IEnumerable<UserDocument>, long)> SearchAsync(
        string keyword, int page, int size)
        {
            var response = await _elasticClient.SearchAsync<UserDocument>(s => s
                .Index(_indexName)
                .From((page - 1) * size)
                .Size(size)
                .Query(q => q
                    .MultiMatch(m => m
                        .Fields(f => f
                            .Field(p => p.FullName)
                            .Field(p => p.Email)
                            .Field(p => p.Username)
                            .Field(p => p.PhoneNumber)
                        )
                        .Query(keyword)
                    )
                )
            );
            if (!response.IsValid)
                throw new Exception(response.DebugInformation);
            return (response.Documents, response.Total);
        }
    }
}
