using PlatformService.Models;
using PlatformServices.Data;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopultion(WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
        }

        private static void SeedData(AppDbContext context)
        {
           if(!context.Platforms.Any())
           {
                Console.WriteLine("-->Seeding Data");
                context.Platforms.AddRange(
                    new Platform(){Name = "Dot Net",Publisher ="Microsoft",Cost = "free"},
                    new Platform(){Name = "SQL Server Express",Publisher ="Microsoft",Cost = "free"},
                    new Platform(){Name = "Kubernetes",Publisher ="Cloud Native Computing Foundation",Cost = "free"}
                );
                context.SaveChanges();

           }else
           {
                Console.WriteLine("-->We already have data");
           }
        }
    }
}