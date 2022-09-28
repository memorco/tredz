namespace Tredz.DataAccess.Sql.Models;

public class DefaultStoredProcedureRequestInOut
{
    public string? StoredProcedureName { get; set; }

    public IEnumerable<StoredProcedureParamsRequestInOut> Parameters { get; set; } = new List<StoredProcedureParamsRequestInOut>();
}

