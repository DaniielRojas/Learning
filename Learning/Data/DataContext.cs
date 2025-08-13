using Learning.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace Learning.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ConversationType> ConversationTypes { get; set; }
        public virtual DbSet<Course> Courses { get; set; }  


    }
}
