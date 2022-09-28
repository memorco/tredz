namespace Tredz.DataAccess.Sql.Models;

public class StoredProcedureParamsRequest
{
    public StoredProcedureParamsRequest()
    {
    }

    public StoredProcedureParamsRequest(string parameterName, string parameterValue)
        : this (parameterName, parameterValue, DbType.String)
    {
    }

    public StoredProcedureParamsRequest(string parameterName, string parameterValue, DbType databaseType = DbType.String)
    {
        ParameterName = parameterName;
        ParameterValue = parameterValue;
        DatabaseType = databaseType;
    }

    public string ParameterName { get; set; }

    public object ParameterValue { get; set; }

    public DbType DatabaseType { get; set; }
}