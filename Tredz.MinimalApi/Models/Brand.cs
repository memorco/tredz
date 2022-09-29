namespace Tredz.MinimalApi.Models;

public class Brand
{
    public int Id { get; set; }

    public string Name { get; set; }

    public bool IsStocked { get; set; }
    public string Description { get; set; }
}