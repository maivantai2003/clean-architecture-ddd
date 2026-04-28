namespace UserDevice.Application.DTOs
{
    public class UserDeviceDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Location { get; set; }
        public string DeviceId { get; set; }
        public string DeviceType { get;set; }
        public string? FirebaseToken { get;set; }
    }
}
