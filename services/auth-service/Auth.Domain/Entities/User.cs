using Auth.Domain.Common;
using Auth.Domain.PasswordResetToken.Events;
using Auth.Domain.Users.Events;
using System.ComponentModel.DataAnnotations;

namespace Auth.Domain.Entities
{
    public class User : AggregateRoot
    {
        [Key]
        public Guid UserId { get; private set; }
        public string Username { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public bool IsActive { get; private set; } = true;
        private readonly List<UserRole> _roles = new();
        public IReadOnlyCollection<UserRole> UserRoles => _roles.AsReadOnly();
        public User() { }
        public User(string username, string email, string passwordHash)
        {
            UserId = Guid.NewGuid();
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            IsActive = true;
            AddDomainEvent(new UserCreatedDomainEvent(UserId,Username,Email));
        }
        public void Deactivate()
        {
            if (IsActive)
            {
                IsActive = false;
                AddDomainEvent(new UserStatusChangedDomainEvent(UserId, false));
            }
        }
        public void Activate()
        {
            if (!IsActive)
            {
                IsActive = true;
                AddDomainEvent(new UserStatusChangedDomainEvent(UserId, true));
            }
        }
        public void ChangePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
        }
        public void UpdateEmail(string newEmail)
        {
            if (newEmail != Email)
            {
                Email = newEmail;
            }
        }
        public void AssignRole(Guid roleId)
        {
            if (_roles.Any(r => r.RoleId == roleId))
                return;
            _roles.Add(new UserRole(UserId, roleId));
            AddDomainEvent(new UserRoleAssignedDomainEvent(UserId, roleId));
        }
        public void RequestPasswordReset(string resetToken,DateTime expiresAtUtc)
        {
            //if (!IsActive)
            //    throw new DomainException(
            //        "Inactive user cannot request password reset.");

            //if (string.IsNullOrWhiteSpace(resetToken))
            //    throw new DomainException(
            //        "Reset token cannot be empty.");

            //if (expiresAtUtc <= DateTime.UtcNow)
            //    throw new DomainException(
            //        "Reset token expiration must be in the future.");

            AddDomainEvent(new PasswordResetRequestedDomainEvent(UserId,Email,resetToken,expiresAtUtc));
        }
        public void RemoveRole(Guid roleId)
        {
            var role = _roles.FirstOrDefault(r => r.RoleId == roleId);
            if (role == null) return;
            _roles.Remove(role);
            AddDomainEvent(new UserRoleRemovedDomainEvent(UserId, roleId));
        }
    }
}
