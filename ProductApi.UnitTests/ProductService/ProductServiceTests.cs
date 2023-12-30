using Moq;
using MediatR;
using FluentValidation;
using Product.Domain.Helpers;
using Product.Domain.Requests;
using Product.Domain.Responses;
using FluentValidation.Results;
using Product.BLL.Queries.ProductQueries.GetById;
using Product.BLL.Commands.ProductCommands.Create;
using Product.BLL.Commands.ProductCommands.Delete;
using Product.BLL.Commands.ProductCommands.Update;
using Product.BLL.Queries.ProductQueries.GetAllProducts;

namespace ProductApi.UnitTests.ProductService;

public class ProductServiceTests
{
    [Fact]
    public async Task CreateProductAsync_ValidRequest_ReturnsProductId()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var validatorMock = new Mock<IValidator<ProductRequest>>();

        validatorMock.Setup(x => x.ValidateAsync(It.IsAny<ProductRequest>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new ValidationResult());

        mediatorMock.Setup(x => x.Send(It.IsAny<CreateProductCommand>(), default))
                    .ReturnsAsync(1);

        var productService = new Product.BLL.Services.ProductService.ProductService(mediatorMock.Object, validatorMock.Object);

        // Act
        var productId = await productService.CreateProductAsync(new ProductRequest());

        // Assert
        Assert.True(productId > 0);
        mediatorMock.Verify(x => x.Send(It.IsAny<CreateProductCommand>(), default), Times.Once);
    }

    [Fact]
    public async Task CreateProductAsync_InvalidRequest_ThrowsException()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var validatorMock = new Mock<IValidator<ProductRequest>>();

        validatorMock.Setup(x => x.ValidateAsync(It.IsAny<ProductRequest>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new ValidationResult(new List<ValidationFailure> { new ValidationFailure("PropertyName", "Error message") }));

        var productService = new Product.BLL.Services.ProductService.ProductService(mediatorMock.Object, validatorMock.Object);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => productService.CreateProductAsync(new ProductRequest()));
        mediatorMock.Verify(x => x.Send(It.IsAny<CreateProductCommand>(), default), Times.Never);
    }

    [Fact]
    public async Task GetAllProductsAsync_ReturnsPagedResult()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var queryParameters = new QueryParameters();
        var expectedResult = new PagedResult<ProductResponse>(); // Mocking the expected result

        mediatorMock.Setup(x => x.Send(It.IsAny<GetAllProductsQuery>(), default))
                    .ReturnsAsync(expectedResult);

        var productService = new Product.BLL.Services.ProductService.ProductService(mediatorMock.Object, Mock.Of<IValidator<ProductRequest>>());

        // Act
        var result = await productService.GetAllProductsAsync(queryParameters);

        // Assert
        Assert.Equal(expectedResult, result);
        mediatorMock.Verify(x => x.Send(It.IsAny<GetAllProductsQuery>(), default), Times.Once);
    }

    [Fact]
    public async Task GetProductByIdAsync_ReturnsProductResponse()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var expectedResult = new ProductResponse(); // Mocking the expected result

        mediatorMock.Setup(x => x.Send(It.IsAny<GetProductByIdQuery>(), default))
                    .ReturnsAsync(expectedResult);

        var productService = new Product.BLL.Services.ProductService.ProductService(mediatorMock.Object, Mock.Of<IValidator<ProductRequest>>());

        // Act
        var result = await productService.GetProductByIdAsync(1);

        // Assert
        Assert.Equal(expectedResult, result);
        mediatorMock.Verify(x => x.Send(It.IsAny<GetProductByIdQuery>(), default), Times.Once);
    }

    [Fact]
    public async Task UpdateProductAsync_ValidRequest_NoExceptionThrown()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var validatorMock = new Mock<IValidator<ProductRequest>>();

        validatorMock.Setup(x => x.ValidateAsync(It.IsAny<ProductRequest>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new ValidationResult());

        var productService = new Product.BLL.Services.ProductService.ProductService(mediatorMock.Object, validatorMock.Object);

        // Act and Assert
        
        await productService.UpdateProductAsync(1, new ProductRequest());
        mediatorMock.Verify(x => x.Send(It.IsAny<UpdateProductCommand>(), default), Times.Once);
    }

    [Fact]
    public async Task UpdateProductAsync_InvalidRequest_ThrowsException()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var validatorMock = new Mock<IValidator<ProductRequest>>();

        validatorMock.Setup(x => x.ValidateAsync(It.IsAny<ProductRequest>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new ValidationResult(new List<ValidationFailure> { new ValidationFailure("PropertyName", "Error message") }));

        var productService = new Product.BLL.Services.ProductService.ProductService(mediatorMock.Object, validatorMock.Object);

        const int productId = 1;
        
        // Act and Assert
        Assert.True(productId > 0);
        await Assert.ThrowsAsync<Exception>(() => productService.UpdateProductAsync(productId, new ProductRequest()));
        mediatorMock.Verify(x => x.Send(It.IsAny<UpdateProductCommand>(), default), Times.Never);
    }

    [Fact]
    public async Task DeleteProductAsync_NoExceptionThrown()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var productService = new Product.BLL.Services.ProductService.ProductService(mediatorMock.Object, Mock.Of<IValidator<ProductRequest>>());

        const int productId = 1;
        
        // Act and Assert
        Assert.True(productId > 0);
        await productService.DeleteProductAsync(productId);
        mediatorMock.Verify(x => x.Send(It.IsAny<DeleteProductCommand>(), default), Times.Once);
    }
}