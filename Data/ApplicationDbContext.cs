using FoodMenu_RazorPages.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodMenu_RazorPages.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Category { get; set; }
    }
}
