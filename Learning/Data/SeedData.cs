using Learning.Data.Seeders;
using Microsoft.EntityFrameworkCore;

namespace Learning.Data
{
    public class SeedData
    {
        public static void Initialize(DataContext context)
        {
            RoleSeeder.Seed(context);
            DocumentTypeSeeder.Seed(context);
            CategorySeeder.Seed(context);
            ConversationTypeSeeder.Seed(context);
            context.Database.Migrate();
        }
    }
}
