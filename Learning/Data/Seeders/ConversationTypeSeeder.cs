using Learning.Models.Entities;
using Learning.Data;
using System.Linq;

namespace Learning.Data.Seeders
{
    public class ConversationTypeSeeder
    {
        public static void Seed(DataContext context)
        {
            if (!context.ConversationTypes.Any())
            {
                context.ConversationTypes.AddRange(
                    new ConversationType { Name = "General" },
                    new ConversationType { Name = "Soporte Técnico" },
                    new ConversationType { Name = "Consultas Académicas" },
                    new ConversationType { Name = "Feedback de Cursos" },
                    new ConversationType { Name = "Sugerencias" }
                );
                context.SaveChanges();
            }
        }

    }
}
