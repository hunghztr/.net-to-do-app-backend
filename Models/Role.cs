using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models
{
    public class Role : IdentityRole
    {
        public string Description { get; set; } = string.Empty;
        public List<RolePermission> rolePermissions { get; set; } = new List<RolePermission>();
    }
}
