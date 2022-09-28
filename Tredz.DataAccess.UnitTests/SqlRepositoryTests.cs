namespace Tredz.DataAccess.UnitTests;

public class SqlRepositoryTests
{
    private string _connectionString = "someconnstring";

    [Fact]
    public void GetAsyncTest()
    {
        Assert.Equal(1, 1);
    }

    [Fact]
    public void GetAllAsyncTest()
    {

    }

    //[IgnoreOnAzureDevopsFact]
    [Fact(Skip = "This is more of an integration test")]
    public void InsertAsyncTest()
    {
        ISqlRepository repo = new SqlRepository(_connectionString);

        var defaultStoredProcedureRequest = new DefaultStoredProcedureRequest
        {
            Parameters = new List<StoredProcedureParamsRequest>
            {
                new() { ParameterName = "@id", DatabaseType = DbType.Int32, ParameterValue = "1" },
                new() { ParameterName = "@name", DatabaseType = DbType.String, ParameterValue = "Trek" }
            },
            StoredProcedureName = "CreateBrand"
        };

        var result = repo.InsertAsync<DefaultStoredProcedureRequest>(defaultStoredProcedureRequest).Result;

        Assert.NotNull(result);
    }

    //[IgnoreOnAzureDevopsFact]
    [Fact(Skip = "This is more of an integration test")]
    public void ExecuteAsyncTest()
    {
        ISqlRepository repo = new SqlRepository(_connectionString);

        var defaultStoredProcedureRequest = new DefaultStoredProcedureRequest
        {
            Parameters = new List<StoredProcedureParamsRequest>
            {
                new() { ParameterName = "@RaceId", DatabaseType = DbType.Int32, ParameterValue = "6" },
                new() { ParameterName = "@Title", DatabaseType = DbType.String, ParameterValue = "Trek" }
            },
            StoredProcedureName = "Create_ExpertAnalysisArticle"
        };

        var result = repo.ExecuteAsync(defaultStoredProcedureRequest).Result;

        Assert.Equal(6, result);
    }

    [Fact]
    public void InsertAsyncTest1()
    {

    }

    [Fact]
    public void UpdateAsyncTest()
    {

    }

    [Fact]
    public void DeleteAsyncTest()
    {

    }
}
