
using System.ComponentModel.DataAnnotations;
namespace Auth.Domain.Entities
{
    public class UserRole
    {
        [Key]
        public Guid UserRoleId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public User User { get; private set; } = null!;
        public UserRole() { }
        public UserRole(Guid userId, Guid roleId)
        {
            UserRoleId= Guid.NewGuid();
            UserId = userId;
            RoleId = roleId;
        }
        public void UpdateRole(Guid newRoleId)
        {
            RoleId = newRoleId;
        }
    }
}
