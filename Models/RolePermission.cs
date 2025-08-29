using Microsoft.AspNetCore.Identity;

namespace ToDoList.Models
{
    public class RolePermission
    {
        public Role Role { get; set; }
        public string RoleId { get; set; }
        public Permission Permission { get; set; }
        public int PermissionId { get; set; }

    }
}
