using FluentAssertions;
using Moq;
using TektonWebAPI.Application.Services;
using TektonWebAPI.Core.Abstractions;
using TektonWebAPI.Core.Entities;
using TektonWebAPI.Core.Utilities;

namespace TektonWebAPI.Tests.Services;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly IProductService _productService;

    public ProductServiceTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _productService = new ProductService(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProduct_WhenProductExists()
    {
        // Arrange
        var productId = 1;
        var product = new Product
        {
            ProductId = productId,
            Name = "Test Product",
            Status = 1,
            Stock = 10,
            Description = "Test Description",
            Price = 100.0m
        };

        _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId))
            .ReturnsAsync(Result<Product>.Success(product));

        // Act
        var result = await _productService.GetByIdAsync(productId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(product);
    }

    [Fact]
    public async Task AddAsync_ShouldReturnSuccess_WhenProductIsValid()
    {
        // Arrange
        var productId = 1;
        var product = new Product
        {
            ProductId = productId,
            Name = "Test Product",
            Status = 1,
            Stock = 10,
            Description = "Test Description",
            Price = 100.0m
        };


        _productRepositoryMock.Setup(repo => repo.AddAsync(product))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await _productService.AddAsync(product);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnSuccess_WhenProductIsValid()
    {
        // Arrange
        var product = new Product
        {
            ProductId = 1,
            Name = "Updated Product",
            Status = 1,
            Stock = 10,
            Description = "Updated Description",
            Price = 100.0m
        };

        _productRepositoryMock.Setup(repo => repo.UpdateAsync(product))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await _productService.UpdateAsync(product);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}