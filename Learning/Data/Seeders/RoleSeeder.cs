using Learning.Models.Entities;
using Learning.Data;
using System.Linq;

namespace Learning.Data.Seeders
{
    public static class RoleSeeder
    {
        public static void Seed(DataContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Role { Name = "Admin" },
                    new Role { Name = "Student" },
                    new Role { Name = "Teacher" }
                );
                context.SaveChanges();
            }
        }
    }
}
