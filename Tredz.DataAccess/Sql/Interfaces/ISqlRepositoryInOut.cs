namespace Tredz.DataAccess.Sql.Interfaces;

public interface ISqlRepositoryInOut
{
    Task<TOuput> GetAsync<TOuput>(DefaultStoredProcedureRequestInOut request);

    Task<IEnumerable<TOuput>> GetAllAsync<TOuput>(DefaultStoredProcedureRequestInOut request);

    Task<TOuput> InsertAsync<TOuput>(DefaultStoredProcedureRequestInOut request);

    Task<TOuput> UpdateAsync<TOuput>(DefaultStoredProcedureRequestInOut request);

    Task DeleteAsync(DefaultStoredProcedureRequestInOut request);

    Task<TOuput> DeleteAsync<TOuput>(DefaultStoredProcedureRequestInOut request);

    Task<int> ExecuteAsync(DefaultStoredProcedureRequestInOut request);
}


