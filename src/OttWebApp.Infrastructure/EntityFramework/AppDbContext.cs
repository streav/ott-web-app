using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OttWebApp.Core.Entity;

namespace OttWebApp.Infrastructure.EntityFramework;

public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}