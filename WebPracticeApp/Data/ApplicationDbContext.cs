using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebPracticeApp.Models;

namespace WebPracticeApp.Data
{
  public class ApplicationDbContext : IdentityDbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<WebPracticeApp.Models.DefaultClass> DefaultClass { get; set; }
  }
}