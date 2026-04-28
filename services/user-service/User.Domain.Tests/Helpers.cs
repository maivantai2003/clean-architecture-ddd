using User.Domain.Entities;

namespace User.Domain.Tests
{
    public static class Helpers
    {
        public static User.Domain.Entities.User CreateUser()
        {
            return new User.Domain.Entities.User(
                Guid.NewGuid(),
                "Van Tai",
                "vantai08122003@gmail.com",
                "Van Tai"
            );
        }

    }
}
