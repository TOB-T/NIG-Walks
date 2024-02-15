using Microsoft.EntityFrameworkCore;
using NIGWalks.API.Models.Domain;

namespace NIGWalks.API.Data
{
    public class NIGWalksDbContext: DbContext
    {
        public NIGWalksDbContext(DbContextOptions<NIGWalksDbContext> Options) : base(Options) 
        {
            
        }

        public DbSet<Difficulty> Difficulties {  get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var Difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("3bb354ce-fcc0-4cf9-8e40-269188b8729e"),
                    Name = "Easy",

                },
                new Difficulty()
                {
                    Id =  Guid.Parse("a0e7007b-1678-4045-a807-cc266ce520cd"),
                    Name = "Medium",
                },
                new Difficulty()
                {
                    Id = Guid.Parse("f1d2780e-4176-4e31-9174-e294f908467c"),
                    Name = "Hard",
                }
            };

            // Send Difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(Difficulties);

            // Seed data for Regions
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("36640363-96c9-4c80-8785-294c25ba44b9"),
                    Name = "Lagos",
                    Code = "LAG",
                    RegionImageUrl = null
                },
                new Region()
                {
                    Id = Guid.Parse("ba384c8f-a9ae-4225-841d-8937b700be33"),
                    Name = "Abuja",
                    Code = "ABJ",
                    RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region()
                {
                    Id = Guid.Parse("d9c9c0dc-c40e-46b5-ae5a-867e774f7d65"),
                    Name = "Benin",
                    Code = "BEN",
                    RegionImageUrl = null
                },
                new Region()
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Warri",
                    Code = "WAR",
                    RegionImageUrl = null
                },
                new Region()
                {
                    Id = Guid.Parse("d9c9c0dc-c40e-46b5-ae5a-867e774f7d55"),
                    Name = "Port-Harcourt",
                    Code = "PH",
                    RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                }


            };

            // Send Difficulties to the database
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
