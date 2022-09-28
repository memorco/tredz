namespace Tredz.DataAccess.Sql.Interfaces;

public interface ISqlRepository
{
    Task<TOuput> GetAsync<TOuput>(DefaultStoredProcedureRequest request);
        
    Task<IEnumerable<TOuput>> GetAllAsync<TOuput>(DefaultStoredProcedureRequest request);

    Task<TOuput> InsertAsync<TOuput>(DefaultStoredProcedureRequest request);

    Task<TOuput> UpdateAsync<TOuput>(DefaultStoredProcedureRequest request);

    Task DeleteAsync(DefaultStoredProcedureRequest request);

    Task<TOuput> DeleteAsync<TOuput>(DefaultStoredProcedureRequest request);

    Task<int> ExecuteAsync(DefaultStoredProcedureRequest request);
}