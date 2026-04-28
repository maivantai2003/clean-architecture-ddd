using Nest;
using Search.Domain.Entities;

namespace Search.Infrastructure.Config
{
    public static class ElasticExtensions
    {
        public static async Task CreateUserIndex(
            IElasticClient client,
            string index)
        {
            var exists = await client.Indices.ExistsAsync(index);
            if (exists.Exists) return;

            await client.Indices.CreateAsync(index, c => c
                .Settings(s => s
                    .NumberOfShards(1)
                    .NumberOfReplicas(0))
                .Map<UserDocument>(m => m
                    .AutoMap()
                    .Properties(p => p
                        .Text(t => t.Name(n => n.FullName))
                        .Text(t => t.Name(n => n.Email)
                            .Fields(f => f.Keyword(k => k.Name("keyword"))))
                        .Keyword(k => k.Name(n => n.Username))
                        .Keyword(k => k.Name(n => n.PhoneNumber))
                    )
                )
            );
        }
    }
}
