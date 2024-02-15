using NIGWalks.API.Models.Domain;
using NIGWalks.API.Models.DTO;

namespace NIGWalks.API.Repositories
{
    public interface IRegionRepository 
    {
       Task<List<Region>> GetAllAsync();
       Task<Region> GetByIdAsync(Guid id);
       Task<Region> CreateAsync(Region region);
       Task<Region?> UpdateAsync(Guid id,Region region);
       Task<Region?> DeleteAsync(Guid id);
    }
}
