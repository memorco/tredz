namespace Tredz.DataAccess.Sql.Repositories;

public class SqlRepository : ISqlRepository
{
    private readonly string _connectionString;

    public SqlRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<TOuput> GetAsync<TOuput>(DefaultStoredProcedureRequest request)
    {
        var connection = GenerateDatabaseConnection();
        await using var _ = connection;
        if (connection.State == ConnectionState.Closed)
        {
            await connection.OpenAsync();
        }

        var response = await connection.QueryAsync<TOuput>(
            request.StoredProcedureName, MapParameters(request.Parameters), commandType: CommandType.StoredProcedure);

        return response.FirstOrDefault();
    }

    public async Task<IEnumerable<TOuput>> GetAllAsync<TOuput>(DefaultStoredProcedureRequest request)
    {
        var connection = GenerateDatabaseConnection();
        await using var _ = connection;
        if (connection.State == ConnectionState.Closed)
        {
            await connection.OpenAsync();
        }

        return await connection.QueryAsync<TOuput>(request.StoredProcedureName, MapParameters(request.Parameters), commandType: CommandType.StoredProcedure);
    }

    public async Task<TOuput> InsertAsync<TOuput>(DefaultStoredProcedureRequest request) => await TransactionalStoredProcedureQuery<TOuput>(request);
        
    public async Task<TOuput> UpdateAsync<TOuput>(DefaultStoredProcedureRequest request) => await TransactionalStoredProcedureQuery<TOuput>(request);
        
    public async Task<TOuput> DeleteAsync<TOuput>(DefaultStoredProcedureRequest request) => await TransactionalStoredProcedureQuery<TOuput>(request);

    public async Task DeleteAsync(DefaultStoredProcedureRequest request)
    {
        await TransactionalStoredProcedureQuery<int>(request);
    }

    public async Task<int> ExecuteAsync(DefaultStoredProcedureRequest request)
    {
        int result;

        await using var connection = GenerateDatabaseConnection();;
        if (connection.State == ConnectionState.Closed)
        {
            await connection
                    .OpenAsync()
                ;
        }

        try
        {
            result = await connection
                    .ExecuteAsync(request.StoredProcedureName, MapParameters(request.Parameters), commandType: CommandType.StoredProcedure)
                ;
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                await connection
                        .CloseAsync()
                    ;
            }
        }

        return result;
    }

    private async Task<TOuput> TransactionalStoredProcedureQuery<TOuput>(DefaultStoredProcedureRequest request)
    {
        TOuput result;

        var connection = GenerateDatabaseConnection();
        await using var _ = connection;
        if (connection.State == ConnectionState.Closed)
        {
            await connection.OpenAsync();
        }

        try
        {
            var transaction = await connection.BeginTransactionAsync();
            await using var __ = transaction;
            try
            {
                var response =
                    await connection.QueryAsync<TOuput>(
                        request.StoredProcedureName, MapParameters(request.Parameters), commandType: CommandType.StoredProcedure, transaction: transaction);

                result = response.FirstOrDefault();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                await connection.CloseAsync();
            }
        }

        return result;
    }

    private SqlConnection GenerateDatabaseConnection() => new(_connectionString);

    private DynamicParameters MapParameters(IEnumerable<StoredProcedureParamsRequest> parameters)
    {
        if (parameters == null || !parameters.Any())
        {
            return null;
        }

        var dynamicParameters = new DynamicParameters();

        foreach (var parameter in parameters)
        {
            dynamicParameters.Add(parameter.ParameterName, parameter.ParameterValue, parameter.DatabaseType);
        }

        return dynamicParameters;
    }
}