using Learning.Models.Entities;
using Learning.Data;
using System.Linq;


namespace Learning.Data.Seeders
{
    public class DocumentTypeSeeder
    {
       public static void Seed(DataContext context)
        {
         
            if (!context.DocumentTypes.Any())
            {
                var document_type = new DocumentType();
                context.DocumentTypes.AddRange(
                    new DocumentType { Name = "Cédula de Ciudadanía", CreatedAt = DateTime.UtcNow },
                    new DocumentType { Name = "Tarjeta de Identidad", CreatedAt = DateTime.UtcNow },
                    new DocumentType { Name = "Registro Civil" , CreatedAt = DateTime.UtcNow },
                    new DocumentType { Name = "Pasaporte", CreatedAt = DateTime.UtcNow },
                    new DocumentType { Name = "Cédula de Extranjería", },
                    new DocumentType { Name = "Número de Identificación Tributaria (NIT)" },
                    new DocumentType { Name = "Permiso Especial de Permanencia (PEP)" },
                    new DocumentType { Name = "Permiso por Protección Temporal (PPT)" },
                    new DocumentType { Name = "Documento Nacional de Identidad Extranjero" },
                    new DocumentType { Name = "Salvoconducto SC-2" }
               
                );
                context.SaveChanges();
            }
        }



    }
}
