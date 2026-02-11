using System.Text.Json.Serialization;

namespace UserApi.Domain.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<User> Users { get; set; } = [];
        public ICollection<Permission> Permissions { get; set; } = [];
    }
}