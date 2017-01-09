using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Promocodoz.Domain.Core.Entities;

namespace Promocodoz.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("ApplicationDbConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Code> Codes { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Code>()
                .HasRequired(x => x.User)
                .WithMany(x => x.Codes)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}
