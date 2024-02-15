using Microsoft.EntityFrameworkCore;
using NIGWalks.API.Data;
using NIGWalks.API.Models.Domain;
using NIGWalks.API.Models.DTO;

namespace NIGWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NIGWalksDbContext _dbContext;

        public SQLRegionRepository(NIGWalksDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();
            return region;
            
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
           var existingDomainModel = await _dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (existingDomainModel == null)
            {
                return null;
            }

            _dbContext.Regions.Remove(existingDomainModel);
            _dbContext.SaveChanges();
            return existingDomainModel;


        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetByIdAsync(Guid id)
        {
            return await _dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion= await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await _dbContext.SaveChangesAsync();
            return existingRegion;

        }



        
    }
}
