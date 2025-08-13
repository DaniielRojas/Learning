using Learning.Models.Entities;
using Learning.Data;
using System.Linq;

namespace Learning.Data.Seeders

{
    public class CategorySeeder
    {
        public static void Seed(DataContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Name = "Programación" },
                    new Category { Name = "Diseño Gráfico" },
                    new Category { Name = "Marketing Digital" },
                    new Category { Name = "Matemáticas" },
                    new Category { Name = "Ciencias Naturales" },
                    new Category { Name = "Historia" },
                    new Category { Name = "Idiomas" },
                    new Category { Name = "Física" },
                    new Category { Name = "Química" },
                    new Category { Name = "Biología" },
                    new Category { Name = "Literatura" },
                    new Category { Name = "Geografía" },
                    new Category { Name = "Filosofía" },
                    new Category { Name = "Psicología" },
                    new Category { Name = "Economía" },
                    new Category { Name = "Contabilidad" },
                    new Category { Name = "Arquitectura" },
                    new Category { Name = "Medicina" },
                    new Category { Name = "Ingeniería" },
                    new Category { Name = "Arte y Cultura" }
                );
                context.SaveChanges();
            }
        }



    }
}
