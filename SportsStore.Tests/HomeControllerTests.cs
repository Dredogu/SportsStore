using Microsoft.AspNetCore.Mvc;
using SportsStore.Controllers;
using SportsStore.Models;
using Moq;

namespace SportsStore.Tests;

public class HomeControllerTests
{
    [Fact]
    public void Can_Use_Repository()
    {
        // Arrange
        Mock<IStoreRepository> mock = new();

        mock.SetupGet(m => m.Products).Returns((new Product[] {
            new Product { ProductID = 1, Name = "P1" },
            new Product { ProductID = 2, Name = "P2" }
        }).AsQueryable<Product>());

        HomeController controller = new(mock.Object);

        // Act
        IEnumerable<Product>? result = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

        // Assert

        Product[]? productArray = result.ToArray() ?? Array.Empty<Product>();

        Assert.Equal(2, productArray.Length);
        Assert.Equal("P1", productArray[0].Name);
        Assert.Equal("P2", productArray[1].Name);
    }
}