using Learning.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace Learning.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) { }

        public virtual DbSet<User> Users { get; set; }


    }
}
