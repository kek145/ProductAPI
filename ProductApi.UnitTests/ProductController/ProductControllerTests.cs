using Microsoft.AspNetCore.Mvc;
using Moq;
using Product.BLL.Services.ProductService;
using Product.Domain.Helpers;
using Product.Domain.Requests;
using Product.Domain.Responses;

namespace ProductApi.UnitTests.ProductController;

public class ProductControllerTests
{
    [Fact]
    public async Task DeleteProduct_ReturnsNoContent()
    {
        // Arrange
        var productServiceMock = new Mock<IProductService>();
        var controller = new Controllers.ProductController(productServiceMock.Object);

        // Act
        var result = await controller.DeleteProduct(1);

        // Assert
        productServiceMock.Verify(x => x.DeleteProductAsync(1), Times.Once);
        
        var noContentResult = Assert.IsType<NoContentResult>(result);

        Assert.Equal(204, noContentResult.StatusCode);
    }
    
    [Fact]
    public async Task UpdateProduct_ReturnsNoContent()
    {
        // Arrange
        var productServiceMock = new Mock<IProductService>();
        var controller = new Controllers.ProductController(productServiceMock.Object);

        // Act
        var result = await controller.UpdateProduct(1, new ProductRequest());

        // Assert
        productServiceMock.Verify(x => x.UpdateProductAsync(1, It.IsAny<ProductRequest>()), Times.Once);

        var noContentResult = Assert.IsType<NoContentResult>(result);
        
        Assert.Equal(204, noContentResult.StatusCode);
    }
    
    [Fact]
    public async Task CreateProductAsync_GetOnCreated_StatusCode201()
    {
        // Arrange
        var productServiceMock = new Mock<IProductService>();
        productServiceMock.Setup(x => x.CreateProductAsync(It.IsAny<ProductRequest>()))
            .ReturnsAsync(1);

        var controller = new Controllers.ProductController(productServiceMock.Object);

        // Act
        var result = await controller.CreateProduct(new ProductRequest());

        // Assert
        productServiceMock.Verify(x => x.CreateProductAsync(It.IsAny<ProductRequest>()), Times.Once);
        
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        
        Assert.Equal(201, createdAtActionResult.StatusCode);
        
        Assert.Equal("GetProductById", createdAtActionResult.ActionName);
        Assert.Equal(1, createdAtActionResult.RouteValues?["productId"]);
    }
    
    [Fact]
    public async Task GetAllTaskAsync_GetOnSuccess_StatusCode200()
    {
        // Arrange
        var productServiceMock = new Mock<IProductService>();
        var controller = new Controllers.ProductController(productServiceMock.Object);
        
        // Act
        var result = await controller.GetAllProducts(new QueryParameters());
        
        // Assert
        productServiceMock.Verify(x => x.GetAllProductsAsync(It.IsAny<QueryParameters>()), Times.Once);
        
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetTaskByIdAsync_GetOnSuccess_StatusCode200()
    {
        // Arrange
        var productServiceMock = new Mock<IProductService>();
        
        productServiceMock.Setup(x => x.GetProductByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new ProductResponse());
        

        var controller = new Controllers.ProductController(productServiceMock.Object);

        // Act
        var result = await controller.GetProductById(1);

        // Assert
        productServiceMock.Verify(x => x.GetProductByIdAsync(1), Times.Once);
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        
        Assert.Equal(200, okResult.StatusCode);
        
        Assert.IsType<ProductResponse>(okResult.Value);
    }
}