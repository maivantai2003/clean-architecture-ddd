using System.ComponentModel.DataAnnotations;
namespace Auth.Domain.Entities
{
    public class PermissionAction
    {
        [Key]
        public Guid ActionId { get; set; }
        public string ActionCode { get; set; } = string.Empty;
        public string ActionName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public PermissionAction() { }

        public PermissionAction(string code, string name, string description)
        {
            ActionId = Guid.NewGuid();
            ActionCode = code;
            ActionName = name;
            Description = description;
        }
        public void Update(string name, string description)
        {
            ActionName = name;
            Description = description;
        }
    }
}
