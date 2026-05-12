using Nest;
using Search.Infrastructure.Config;

namespace Search.Infrastructure.Elasticsearch
{
    public class ElasticInitializer: IElasticInitializer
    {
        private readonly IElasticClient _client;
        private readonly ElasticOptions _options;

        public ElasticInitializer(IElasticClient client, ElasticOptions options)
        {
            _client = client;
            _options = options;
        }

        public async Task InitializeAsync()
        {
            await ElasticExtensions.CreateUserIndex(_client, _options.UserIndex);
        }
    }
}
