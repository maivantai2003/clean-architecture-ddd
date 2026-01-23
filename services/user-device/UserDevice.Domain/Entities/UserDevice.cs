namespace UserDevice.Domain.Entities
{
    public class UserDevice
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Location { get; private set; }
        public string DeviceId { get; private set; }
        public string DeviceType { get; private set; }
        public string? FirebaseToken { get; private set; }
        public bool IsTrusted { get; private set; }
        public DateTime LastLoginAt { get; private set; }

        private UserDevice() { }

        public static UserDevice Create(Guid userId, string deviceId, string deviceType, string? location, string? firebaseToken)
        {
            var device = new UserDevice
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                DeviceId = deviceId,
                DeviceType = deviceType,
                FirebaseToken = firebaseToken,
                Location = location ?? "Unknown",
                IsTrusted = false,
                LastLoginAt = DateTime.UtcNow
            };
            return device;
        }

        public void MarkTrusted()
        => IsTrusted = true;

        public void UpdateFirebaseToken(string token)
            => FirebaseToken = token;

        public void UpdateLastLogin()
            => LastLoginAt = DateTime.UtcNow;
    }
}
