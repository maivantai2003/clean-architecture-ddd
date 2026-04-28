

namespace Search.Domain.Entities
{
    public class UserDocument
    {
        public string UserId { get; private set; }
        public string? Username { get; private set; } = null!;
        public string? Email { get; private set; } = null!;
        public string? PhoneNumber { get; private set; }
        public string? FullName { get; private set; } = null!;
        public DateTime? DateOfBirth { get; private set; }
        public UserDocument(string userId, string? username, string? email)
        {
            UserId = userId;
            Username = username;
            Email = email;
        }
    }
}
