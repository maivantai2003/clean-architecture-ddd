namespace UserDevice.Domain.Entities
{
    public class DeviceOtp
    {
        public Guid Id { get; private set; }
        public Guid UserDeviceId { get; private set; }
        public string Code { get; private set; } = null!;
        public DateTime ExpiredAt { get; private set; }
        public bool IsUsed { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private DeviceOtp() { }

        public static DeviceOtp Create(Guid userDeviceId, string code, int expireMinutes = 5)
        {
            return new DeviceOtp
            {
                Id = Guid.NewGuid(),
                UserDeviceId = userDeviceId,
                Code = code,
                CreatedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddMinutes(expireMinutes),
                IsUsed = false
            };
        }

        public void MarkUsed()
        {
            if (IsUsed)
                throw new InvalidOperationException("OTP already used.");

            IsUsed = true;
        }

        public void Validate(string inputCode)
        {
            if (IsUsed)
                throw new InvalidOperationException("OTP already used.");

            if (DateTime.UtcNow > ExpiredAt)
                throw new InvalidOperationException("OTP expired.");

            if (!string.Equals(Code, inputCode))
                throw new InvalidOperationException("OTP is invalid.");
        }
    }
}
