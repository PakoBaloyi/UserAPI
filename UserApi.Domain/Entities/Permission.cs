using System.Text.Json.Serialization;

namespace UserApi.Domain.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<Group> Groups { get; set; } = [];
    }
}