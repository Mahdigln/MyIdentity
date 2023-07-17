using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyIdentity.Models.Entities;

namespace MyIdentity.Data;

public class DataBaseContext : IdentityDbContext<User, Role, string>
{

    public DataBaseContext(DbContextOptions<DataBaseContext> options)
        : base(options)
    {
    }

    public DbSet<Blog> Blogs { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(p => new {p.ProviderKey, p.LoginProvider});
        modelBuilder.Entity<IdentityUserRole<string>>().HasKey(p => new {p.UserId, p.RoleId});
        modelBuilder.Entity<IdentityUserToken<string>>().HasKey(p => new {p.UserId, p.LoginProvider});
        modelBuilder.Entity<IdentityUserToken<string>>().HasKey(t => new {t.UserId, t.LoginProvider, t.Name});


        //modelBuilder.Entity<User>().Ignore(p => p.NormalizedEmail);}
    }
}