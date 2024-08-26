using AutoMapper;
using FluentAssertions;
using Moq;
using TektonWebAPI.Application.DTOs;
using TektonWebAPI.Application.Features.Products.Commands;
using TektonWebAPI.Application.Features.Products.Handlers;
using TektonWebAPI.Core.Entities;
using TektonWebAPI.Core.Interfaces;
using TektonWebAPI.Core.Utilities;

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
}