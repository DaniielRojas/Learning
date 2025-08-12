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
                    new Role { Name = "Administrador" },
                    new Role { Name = "Estudiante" },
                    new Role { Name = "Instructor" }
                );
                context.SaveChanges();
            }
        }
    }
}
