using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoList.Models;
using ToDoList.Utils.Enum;

namespace ToDoList.Data
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<ToDoTask> ToDoTasks { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            List<Role> roles = new List<Role>
            {
                new Role
                {
                    Id = "1",
                    Name = "User",
                    NormalizedName = "USER",
                    Description = "Default role for new users"
                },
                new Role
                {
                    Id = "2",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Description = "Administrator role with full permissions"
                }
            };
            modelBuilder.Entity<RolePermission>().HasKey(rp => new { rp.RoleId, rp.PermissionId });
            modelBuilder.Entity<RolePermission>().HasOne(rp => rp.Role).WithMany().HasForeignKey(rp => rp.RoleId);
            modelBuilder.Entity<RolePermission>().HasOne(rp => rp.Permission).WithMany(p => p.rolePermissions).HasForeignKey(rp => rp.PermissionId);

            List<Permission> permissions = new List<Permission>
            {
                new Permission{Id=1,Name="Create Task",Module=ModuleEnum.ToDoTask.ToString(),Method=MethodEnum.Post.ToString(),Path="/api/tasks"},
                new Permission{Id=2,Name="Get All Tasks",Module=ModuleEnum.ToDoTask.ToString(),Method=MethodEnum.Get.ToString(),Path="/api/tasks"},
                new Permission{Id=3,Name="Get Task By Id",Module=ModuleEnum.ToDoTask.ToString(),Method=MethodEnum.Get.ToString(),Path="/api/tasks/{id}"},
                new Permission{Id=4,Name="Update Task",Module=ModuleEnum.ToDoTask.ToString(),Method=MethodEnum.Put.ToString(),Path="/api/tasks/{id}"},
                new Permission{Id=5,Name="Delete Task",Module=ModuleEnum.ToDoTask.ToString(),Method = MethodEnum.Delete.ToString(),Path="/api/tasks/{id}"},
                new Permission{Id=6,Name="Get All By Username",Module=ModuleEnum.ToDoTask.ToString(),Method=MethodEnum.Get.ToString(),Path="/api/tasks/get-by-user"}
            };
            List<RolePermission> rolePermissions = new List<RolePermission>
            {
                // User role permissions
                new RolePermission{RoleId="1",PermissionId=1},
                new RolePermission{RoleId="1",PermissionId=3},
                new RolePermission{RoleId="1",PermissionId=4},
                new RolePermission{RoleId="1",PermissionId=5},
                new RolePermission{RoleId="1",PermissionId=6},
                // Admin role permissions
                new RolePermission{RoleId="2",PermissionId=1},
                new RolePermission{RoleId="2",PermissionId=2},
                new RolePermission{RoleId="2",PermissionId=3},
                new RolePermission{RoleId="2",PermissionId=4},
                new RolePermission{RoleId="2",PermissionId=5},
                new RolePermission{RoleId="2",PermissionId=6},
            };
            User admin = new User
            {
                Id = "ad",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                SecurityStamp = Guid.NewGuid().ToString("D"),
            };
            admin.PasswordHash = new PasswordHasher<User>().HashPassword(null, "123");
            IdentityUserRole<string> adminRole = new IdentityUserRole<string>
            {
                RoleId = "2",
                UserId = "ad"
            };
       
            modelBuilder.Entity<Permission>().HasData(permissions);
            modelBuilder.Entity<Role>().HasData(roles);
            modelBuilder.Entity<RolePermission>().HasData(rolePermissions);
            modelBuilder.Entity<User>().HasData(admin);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(adminRole);

        }
    }
}
