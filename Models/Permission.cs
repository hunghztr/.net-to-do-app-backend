using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;
using ToDoList.Utils.Enum;

namespace ToDoList.Models
{
    [Table("Permissions")]
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Module { get; set; }
        public string Method { get; set; }
        public string Path { get; set; } = string.Empty;

        [JsonIgnore]
        public List<RolePermission> rolePermissions { get; set; } = new List<RolePermission>();

    }
}
