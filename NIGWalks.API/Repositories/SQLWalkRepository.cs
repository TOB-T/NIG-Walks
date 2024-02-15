using Microsoft.EntityFrameworkCore;
using NIGWalks.API.Data;
using NIGWalks.API.Models.Domain;

namespace NIGWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NIGWalksDbContext _dbContext;

        public SQLWalkRepository(NIGWalksDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid Id)
        {
           var domainModel = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == Id);
            if (domainModel == null)
            {
                return null;
            }
            _dbContext.Walks.Remove(domainModel);
            await _dbContext.SaveChangesAsync();
            return domainModel;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walks = _dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            // Filtering
            if(string.IsNullOrEmpty(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains( filterQuery));
                }

            }

            // sorting
            if (string.IsNullOrEmpty(sortBy) == false) 
            { 
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name): walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination
            var skipResults = (pageNumber-1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();

            // return await _dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid Id)
        {
            var domainModel = await _dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == Id);

            if(domainModel == null)
            {
                return null;
            }
            return domainModel;

        }

        public async Task<Walk?> UpdateAsync(Guid Id, Walk walk)
        {
            var domainModel = await _dbContext.Walks.FirstOrDefaultAsync(x=> x.Id == Id);
            if(domainModel == null)
            {
                return null;
            }
            
            domainModel.Name = walk.Name;
            domainModel.Description = walk.Description;
            domainModel.LengthInKm = walk.LengthInKm;
            domainModel.WalkImageUrl = walk.WalkImageUrl;
            domainModel.DifficultyId = walk.DifficultyId;
            domainModel.RegionId = walk.RegionId;

            await _dbContext.SaveChangesAsync();
            return domainModel;
            
            
        }
    }
}
