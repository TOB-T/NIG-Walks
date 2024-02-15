using NIGWalks.API.Models.Domain;

namespace NIGWalks.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
