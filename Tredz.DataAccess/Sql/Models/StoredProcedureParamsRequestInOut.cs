namespace Tredz.DataAccess.Sql.Models;

public class StoredProcedureParamsRequestInOut
{
    public string? ParameterName { get; set; }

    public object? ParameterValue { get; set; }

    public DbType DatabaseType { get; set; }

    public ParameterDirection Direction { get; set; }
}

