namespace Tredz.DataAccess.Sql.Repositories;

public class SqlRepositoryInOut : ISqlRepositoryInOut
{
    private readonly string _connectionString;

    public SqlRepositoryInOut(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<TOutput> GetAsync<TOutput>(DefaultStoredProcedureRequestInOut request)
    {
        var connection = GenerateDatabaseConnection();
        await using var _ = connection;
        if (connection.State == ConnectionState.Closed)
        {
            await connection.OpenAsync();
        }

        var parameters = MapParameters(request.Parameters);

        var response = await connection.QueryAsync<TOutput>(
            request.StoredProcedureName, parameters, commandType: CommandType.StoredProcedure);

        UpdateParameters(request, parameters);

        return response.FirstOrDefault();
    }

    public async Task<IEnumerable<TOutput>> GetAllAsync<TOutput>(DefaultStoredProcedureRequestInOut request)
    {
        var connection = GenerateDatabaseConnection();
        await using var _ = connection;
        if (connection.State == ConnectionState.Closed)
        {
            await connection.OpenAsync();
        }

        var parameters = MapParameters(request.Parameters);

        var result = await connection.QueryAsync<TOutput>(request.StoredProcedureName, parameters,
            commandType: CommandType.StoredProcedure);
        UpdateParameters(request, parameters);

        return result;
    }

    public async Task<TOutput> InsertAsync<TOutput>(DefaultStoredProcedureRequestInOut request) => await TransactionalStoredProcedureQuery<TOutput>(request);

    public async Task<TOutput> UpdateAsync<TOutput>(DefaultStoredProcedureRequestInOut request) => await TransactionalStoredProcedureQuery<TOutput>(request);

    public async Task<TOutput> DeleteAsync<TOutput>(DefaultStoredProcedureRequestInOut request) => await TransactionalStoredProcedureQuery<TOutput>(request);

    public async Task DeleteAsync(DefaultStoredProcedureRequestInOut request)
    {
        await TransactionalStoredProcedureQuery<int>(request);
    }

    public async Task<int> ExecuteAsync(DefaultStoredProcedureRequestInOut request)
    {
        int result;

        await using var connection = GenerateDatabaseConnection();
        if (connection.State == ConnectionState.Closed)
        {
            await connection.OpenAsync();
        }

        try
        {
            var parameters = MapParameters(request.Parameters);

            result = await connection
                .ExecuteAsync(request.StoredProcedureName, parameters, commandType: CommandType.StoredProcedure);

            UpdateParameters(request, parameters);

            return result;
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                await connection.CloseAsync();
            }
        }
    }

    private void UpdateParameters(DefaultStoredProcedureRequestInOut request, DynamicParameters parameters)
    {
        foreach (var parameter in request.Parameters)
        {
            if (parameter.Direction == ParameterDirection.Output ||
                parameter.Direction == ParameterDirection.InputOutput)
            {
                parameter.ParameterValue = parameters.Get<object>(parameter.ParameterName);
            }
        }
    }

    private async Task<TOutput> TransactionalStoredProcedureQuery<TOutput>(DefaultStoredProcedureRequestInOut request)
    {
        TOutput result;

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
                var parameters = MapParameters(request.Parameters);

                var response =
                    await connection.QueryAsync<TOutput>(
                        request.StoredProcedureName, parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                UpdateParameters(request, parameters);

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

    private DynamicParameters MapParameters(IEnumerable<StoredProcedureParamsRequestInOut> parameters)
    {
        if (parameters == null || !parameters.Any())
        {
            return null!;
        }

        var dynamicParameters = new DynamicParameters();

        foreach (var parameter in parameters)
        {
            dynamicParameters.Add(parameter.ParameterName, parameter.ParameterValue, parameter.DatabaseType, parameter.Direction);
        }

        return dynamicParameters;
    }
}
