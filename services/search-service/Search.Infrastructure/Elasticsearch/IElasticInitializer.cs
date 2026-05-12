namespace Search.Infrastructure.Elasticsearch
{
    public interface IElasticInitializer
    {
        Task InitializeAsync();
    }
}
