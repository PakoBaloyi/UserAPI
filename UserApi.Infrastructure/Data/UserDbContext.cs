using Microsoft.EntityFrameworkCore;
using UserApi.Domain.Entities;

namespace UserApi.Infrastructure.Data
{
    public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(u => u.IsActive);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Groups)
                .WithMany(g => g.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserGroups",
                    j => j.HasOne<Group>().WithMany().HasForeignKey("GroupsId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("UsersId"),
                    j =>
                    {
                        j.HasKey("UsersId", "GroupsId");
                        j.ToTable("UserGroups");
                        j.HasData(new { UsersId = 1, GroupsId = 1 });
                    }
                );

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Permissions)
                .WithMany(p => p.Groups)
                .UsingEntity<Dictionary<string, object>>(
                    "GroupPermissions",
                    j => j.HasOne<Permission>().WithMany().HasForeignKey("PermissionsId"),
                    j => j.HasOne<Group>().WithMany().HasForeignKey("GroupsId"),
                    j =>
                    {
                        j.HasKey("GroupsId", "PermissionsId");
                        j.ToTable("GroupPermissions");
                        j.HasData(
                            new { GroupsId = 1, PermissionsId = 1 },
                            new { GroupsId = 2, PermissionsId = 2 }, 
                            new { GroupsId = 3, PermissionsId = 3 }
                        );


                    }
                );

            modelBuilder.Entity<Group>().HasData(
                new Group { Id = 1, Name = "Administrators" },
                new Group { Id = 2, Name = "Editors" },
                new Group { Id = 3, Name = "Viewers" }


            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", Email = "admin@example.com", Name = "System", LastName = "Admin", IsActive = true }
            );

            modelBuilder.Entity<Permission>().HasData(
                new Permission { Id = 1, Name = "Admin" },
                new Permission { Id = 2, Name = "Editor" },
                new Permission { Id = 3, Name = "Viewer" }
            );
        }
    }
}  