using Realtime.Application.Interfaces;
using StackExchange.Redis;

namespace Realtime.Infrastructure.Stores
{
    public class RedisUserConnectionStore:IUserConnectionStore
    {

        private readonly IDatabase _db;
        private const string Prefix = "realtime:user";

        public RedisUserConnectionStore(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }

        private static string Key(Guid userId)
            => $"{Prefix}:{userId}:connections";

        public async Task AddAsync(Guid userId, string connectionId)
        {
            var key = $"user:{userId}:connections";

            Console.WriteLine($"🧠 REDIS ADD");
            Console.WriteLine($"Key = {key}");
            Console.WriteLine($"ConnectionId = {connectionId}");
            await _db.SetAddAsync(Key(userId), connectionId);
            await _db.KeyExpireAsync(Key(userId), TimeSpan.FromDays(1));
        }

        public async Task RemoveAsync(Guid userId, string connectionId)
        {
            await _db.SetRemoveAsync(Key(userId), connectionId);

            if (await _db.SetLengthAsync(Key(userId)) == 0)
            {
                await _db.KeyDeleteAsync(Key(userId));
            }
        }

        public async Task<IReadOnlyList<string>> GetConnectionsAsync(Guid userId)
        {
            var values = await _db.SetMembersAsync(Key(userId));
            return values.Select(v => v.ToString()).ToList();
        }
    }
}
