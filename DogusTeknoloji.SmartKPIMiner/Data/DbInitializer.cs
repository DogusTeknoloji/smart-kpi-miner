using Microsoft.EntityFrameworkCore;

namespace DogusTeknoloji.SmartKPIMiner.Data
{
    public static class DbInitializer
    {
        public static void Initialize(string connectionString)
        {
            using (SmartKPIDbContext context = new SmartKPIDbContext(connectionString))
            {
                context.Database.Migrate();
            }
        }
    }
}
