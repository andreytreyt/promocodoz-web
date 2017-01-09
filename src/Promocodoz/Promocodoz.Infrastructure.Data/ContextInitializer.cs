using System.Data.Entity;

namespace Promocodoz.Infrastructure.Data
{
    public class ContextInitializer
    : MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>
    {

    }
}