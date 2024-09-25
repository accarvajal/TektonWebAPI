using AutoMapper;
using FluentAssertions;
using Moq;
using TektonWebAPI.Application.DTOs;
using TektonWebAPI.Application.Features.Products.Commands;
using TektonWebAPI.Application.Features.Products.Handlers;
using TektonWebAPI.Core.Abstractions;
using TektonWebAPI.Core.Common;
using TektonWebAPI.Core.Entities;
using TektonWebAPI.Core.Errors;

namespace TektonWebAPI.Tests.Features;

public class AddProductCommandHandlerTests
{
    private readonly Mock<IProductService> _productServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly AddProductCommandHandler _handler;

    public AddProductCommandHandlerTests()
    {
        _productServiceMock = new Mock<IProductService>();
        _mapperMock = new Mock<IMapper>();
        _handler = new AddProductCommandHandler(_productServiceMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenProductIsValid()
    {
        // Arrange
        var productDto = new ProductRequestDto
        {
            ProductId = 1,
            Name = "New Product",
            Status = 1,
            Stock = 10,
            Description = "New Description",
            Price = 100.0m
        };
        var product = new Product
        {
            ProductId = 1,
            Name = "New Product",
            Status = 1,
            Stock = 10,
            Description = "New Description",
            Price = 100.0m
        };
        var command = new AddProductCommand(productDto);

        _mapperMock.Setup(mapper => mapper.Map<Product>(productDto))
            .Returns(product);
        _productServiceMock.Setup(service => service.AddAsync(product))
            .ReturnsAsync(Result<int>.Success(product.ProductId));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(product.ProductId);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenProductIdIsDuplicate()
    {
        // Arrange
        var productDto = new ProductRequestDto
        {
            ProductId = 1,
            Name = "Duplicate Product",
            Status = 1,
            Stock = 10,
            Description = "Duplicate Description",
            Price = 100.0m
        };
        var product = new Product
        {
            ProductId = 1,
            Name = "Duplicate Product",
            Status = 1,
            Stock = 10,
            Description = "Duplicate Description",
            Price = 100.0m
        };
        var command = new AddProductCommand(productDto);

        _mapperMock.Setup(mapper => mapper.Map<Product>(productDto))
            .Returns(product);
        _productServiceMock.Setup(service => service.AddAsync(product))
            .ReturnsAsync(Result<int>.Failure(ProductErrors.AlreadyExists(product.ProductId)));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Code.Should().Be(ProductErrors.ProductAlreadyExists);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenProductIsInvalid()
    {
        // Arrange
        var productDto = new ProductRequestDto
        {
            ProductId = 1,
            Name = "", // Invalid name
            Status = 1,
            Stock = 10,
            Description = "Invalid Description",
            Price = 100.0m
        };
        var product = new Product
        {
            ProductId = 1,
            Name = "", // Invalid name
            Status = 1,
            Stock = 10,
            Description = "Invalid Description",
            Price = 100.0m
        };
        var command = new AddProductCommand(productDto);

        _mapperMock.Setup(mapper => mapper.Map<Product>(productDto))
            .Returns(product);
        _productServiceMock.Setup(service => service.AddAsync(product))
            .ReturnsAsync(Result<int>.Failure(CommonErrors.GeneralError("Invalid product data")));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Description.Should().Be("Invalid product data");
    }
}