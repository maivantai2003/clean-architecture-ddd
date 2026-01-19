using System.ComponentModel.DataAnnotations;
namespace Auth.Domain.Entities
{
    public class Resource
    {
        [Key]
        public Guid ResourceId { get; set; }
        public string ResourceCode { get; set; } = string.Empty;
        public string ResourceName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        protected Resource() { }

        public Resource(string code, string name, string description)
        {
            ResourceId = Guid.NewGuid();
            ResourceCode = code;
            ResourceName = name;
            Description = description;
        }
        public void Update(string name, string description)
        {
            ResourceName = name;
            Description = description;
        }
        
    }
}
