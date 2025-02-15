using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Persistence.DbContext;

using Microsoft.EntityFrameworkCore;
public class UserManagementDbContext : DbContext
{
    public DbSet<UserAccount> UserAccount { get; set; }
    public DbSet<UserRole> UserRole { get; set; }
    public DbSet<UserProfile> UserProfile { get; set; }
    public DbSet<UserProfileUpdates> UserProfileUpdate { get; set; }
    public DbSet<UserSession> UserSession { get; set; }

   
    public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options): base(options)
    {
        
    }

  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<UserRole>().HasData(
            new UserRole
            {
                Id = 1, 
                Name = "SuperAdmin",
            },
            new UserRole{Id = 2, Name = "Admin"},
            new UserRole{Id = 3, Name = "Employee"}
        );
        
        var userId = Guid.NewGuid();
        
      
        modelBuilder.Entity<UserAccount>().HasData(
            new UserAccount()
            {
                Id = 1,
                Email = "superadmin@gmail.com",
                IsActive =  true,
                IsTempPassword = false,
                Password = "$2a$11$lEbCVV77QIL6YYkUGmn3A.fOEkvy9nmnPcDJhKz9GzG4BvTPJdu5a", //123456
                UserId = userId,
                UserRoleId = 1,
                CreatedAt = DateTime.Now
            }
        );
 
        
        modelBuilder.Entity<UserProfileUpdates>()
                    .Property(u=>u.DateOfBirth)
                    .HasColumnType("date");
        
        modelBuilder.Entity<UserProfile>()
            .Property(u=>u.DateOfBirth)
            .HasColumnType("date");

        
        
        modelBuilder.Entity<UserProfile>().HasData(
            new UserProfile()
            {
                Id = 1,
                UserId = userId,
                UserAccountId = 1,
                FirstName = "Super",
                LastName = "Admin",
                DateOfBirth = new DateTime(1999, 02, 11),
            }
        );
      

    }
}