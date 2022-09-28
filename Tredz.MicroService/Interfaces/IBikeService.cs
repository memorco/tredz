namespace Tredz.MicroService.Interfaces;

public interface IBikeService
{
    Task<IEnumerable<Brand>> GetBrandsAsync();

    Task<Brand?> GetBrandByIdAsync(int id);

    Task<bool> UpdateBrandAsync(Brand brand);

    Task<Brand> CreateBrandAsync(Brand brand);

    Task DeleteBrandAsync(int id);
}
