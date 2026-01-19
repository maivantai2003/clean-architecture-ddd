using User.Domain.Common;
using User.Domain.Enums;
using User.Domain.Users.Events;

namespace User.Domain.Entities
{
    public class User:AggregateRoot
    {
        public Guid UserId { get; private set; }
        public string Username { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string? PhoneNumber { get; private set; }
        public string FullName { get; private set; } = null!;
        public string? AvatarUrl { get; private set; } = null;

        public Gender Gender { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        public UserStatus Status { get; private set; }
        private User() { }

        public User(Guid userId, string username, string email, string fullName)
        {
            UserId = userId;
            Username = username;
            Email = email;
            FullName = fullName;
            Status= UserStatus.Active;
        }
        public void UpdateProfile(string fullName,string phoneNumber,string? avatarUrl,Gender gender,DateTime? dateOfBirth)
        {
            FullName = fullName;
            AvatarUrl = avatarUrl;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
        }
        public void UpdateEmail(string newEmail)
        {
            if(newEmail!= Email)
            {
                Email = newEmail;
                AddDomainEvent(new UserEmailUpdatedDomainEvent(UserId, newEmail));
            }
        }
        public void Lock()
        {
            Status = UserStatus.Locked;
        }

        public void Deactivate()
        {
            Status = UserStatus.Inactive;
        }
    }
}
