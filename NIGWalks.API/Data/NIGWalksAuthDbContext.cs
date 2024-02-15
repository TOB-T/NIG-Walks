using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NIGWalks.API.Data
{
    public class NIGWalksAuthDbContext : IdentityDbContext
    {
        public NIGWalksAuthDbContext(DbContextOptions<NIGWalksAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "a33383bb-b9e5-488c-8a52-f94a302ad90d";
            var WriterRoleId = "7aa19030-bd5c-43dc-aea6-da74f36a526f";

            var roleModels = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },

                 new IdentityRole()
                {
                    Id = WriterRoleId,
                    ConcurrencyStamp = WriterRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }

            };

            builder.Entity<IdentityRole>().HasData(roleModels);
        }
    }
}
