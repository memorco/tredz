using Tredz.DataAccess.Sql.Interfaces;
using Tredz.DataAccess.Sql.Models;

namespace Tredz.MicroService.UnitTests;

public class BikeServiceTests
{
    private readonly List<Brand> _brands = new() { new Brand { Id = 1, Name = "Specialized" }, new Brand { Id = 2, Name = "Orbea" } };

    [Fact]
    public async void GetBrandsAsyncTest()
    {
        // Arrange
        var mockRepo = new Mock<ISqlRepository>();
        mockRepo.Setup(s => s.GetAllAsync<Brand>(It.IsAny<DefaultStoredProcedureRequest>())).ReturnsAsync(_brands);

        var sut = new BikeService(mockRepo.Object);
        var expected = _brands;

        // Act
        var result = await sut.GetBrandsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expected.Count, result.Count());
    }

    [Fact]
    public async void GetBrandByIdAsyncTest()
    {
        // Arrange
        var mockRepo = new Mock<ISqlRepository>();
        mockRepo.Setup(s => s.GetAsync<Brand>(It.IsAny<DefaultStoredProcedureRequest>())).ReturnsAsync(_brands.Single(b => b.Id == 1));

        var sut = new BikeService(mockRepo.Object);
        var expected = _brands.Single(b => b.Id == 1);

        // Act
        var result = await sut.GetBrandByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expected.Id, result.Id);
        Assert.Equal(expected.Name, result.Name);
        Assert.Equal(expected.IsStocked, result.IsStocked);
    }
}
