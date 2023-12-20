using FoodMenu.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodMenu.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Category { get; set; }
    }
}
