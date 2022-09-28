using System.Data;

namespace Tredz.MicroService;

public class BikeService : IBikeService
{
    private readonly ISqlRepository _sqlRepository;

    public BikeService(ISqlRepository sqlRepository)
    {
        _sqlRepository = sqlRepository;
    }

    public async Task<IEnumerable<Brand>> GetBrandsAsync()
    {
        var request = new DefaultStoredProcedureRequest
        {
            StoredProcedureName = "GetBrands"
        };

        //var brands = new List<Brand> { new() { Id = 1, Name = "Specialized" }, new() { Id = 2, Name = "Orbea" } };

        //return brands;

        return await _sqlRepository.GetAllAsync<Brand>(request);
    }

    public async Task<Brand?> GetBrandByIdAsync(int id)
    {
        var request = new DefaultStoredProcedureRequest
        {
            StoredProcedureName = "GetBrands",
            Parameters = new List<StoredProcedureParamsRequest> { new() { ParameterName = "id" } }
        };

        //var brands = new List<Brand> { new() { Id = 1, Name = "Specialized" }, new() { Id = 2, Name = "Orbea" } };

        //return brands.First(b => b.Id == id);

        return await _sqlRepository.GetAsync<Brand>(request);
    }

    public async Task<bool> UpdateBrandAsync(Brand brand)
    {
        var request = new DefaultStoredProcedureRequest
        {
            StoredProcedureName = "UpdateBrand",
            Parameters = new List<StoredProcedureParamsRequest>
            {
                new() {ParameterName = "id", ParameterValue = brand.Id, DatabaseType = DbType.Int32},
                new() {ParameterName = "name", ParameterValue = brand.Name, DatabaseType = DbType.String},
                new() {ParameterName = "isStocked", ParameterValue = brand.IsStocked, DatabaseType = DbType.Boolean}
            }
        };

        return await _sqlRepository.DeleteAsync<bool>(request);
    }

    public async Task<Brand> CreateBrandAsync(Brand brand)
    {
        var request = new DefaultStoredProcedureRequest
        {
            StoredProcedureName = "CreateBrand",
            Parameters = new List<StoredProcedureParamsRequest>
            {
                new() {ParameterName = "name", ParameterValue = brand.Name, DatabaseType = DbType.String},
                new() {ParameterName = "isStocked", ParameterValue = brand.IsStocked, DatabaseType = DbType.Boolean}
            }
        };

        return await _sqlRepository.InsertAsync<Brand>(request);
    }

    public async Task DeleteBrandAsync(int id)
    {
        var request = new DefaultStoredProcedureRequest
        {
            StoredProcedureName = "DeleteBrand",
            Parameters = new List<StoredProcedureParamsRequest> { new() { ParameterName = "id" } }
        };

        await _sqlRepository.DeleteAsync(request);
    }
}