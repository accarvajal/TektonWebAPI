using AutoMapper;
using FluentAssertions;
using Moq;
using TektonWebAPI.Application.DTOs;
using TektonWebAPI.Application.Features.Products.Commands;
using TektonWebAPI.Application.Features.Products.Handlers;
using TektonWebAPI.Core.Abstractions;
using TektonWebAPI.Core.Entities;
using TektonWebAPI.Core.Utilities;

namespace TektonWebAPI.Tests.Features;

public class UpdateProductCommandHandlerTests
{
    private readonly Mock<IProductService> _productServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UpdateProductCommandHandler _handler;

    public UpdateProductCommandHandlerTests()
    {
        _productServiceMock = new Mock<IProductService>();
        _mapperMock = new Mock<IMapper>();
        _handler = new UpdateProductCommandHandler(_productServiceMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenProductIsValid()
    {
        // Arrange
        var productDto = new ProductRequestDto
        {
            ProductId = 1,
            Name = "Updated Product",
            Status = 1,
            Stock = 10,
            Description = "Updated Description",
            Price = 150.0m
        };
        var product = new Product
        {
            ProductId = 1,
            Name = "Updated Product",
            Status = 1,
            Stock = 10,
            Description = "Updated Description",
            Price = 150.0m
        };
        var command = new UpdateProductCommand(1, productDto);

        _mapperMock.Setup(mapper => mapper.Map<Product>(productDto))
            .Returns(product);
        _productServiceMock.Setup(service => service.UpdateAsync(product))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenProductIdMismatch()
    {
        // Arrange
        var productDto = new ProductRequestDto
        {
            ProductId = 1,
            Name = "Updated Product",
            Status = 1,
            Stock = 10,
            Description = "Updated Description",
            Price = 150.0m
        };
        var command = new UpdateProductCommand(2, productDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("Product ID mismatch.");
    }
}