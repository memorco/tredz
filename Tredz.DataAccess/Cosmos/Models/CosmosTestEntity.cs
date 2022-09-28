namespace Tredz.DataAccess.Cosmos.Models;

public class CosmosTestEntity : CosmosEntityBase
{
    public string Name { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
}