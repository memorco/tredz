namespace Tredz.DataAccess.Sql.Models;

public class DefaultStoredProcedureRequest
{
    public string StoredProcedureName { get; set; }

    public IEnumerable<StoredProcedureParamsRequest> Parameters { get; set; } = new List<StoredProcedureParamsRequest>();
}