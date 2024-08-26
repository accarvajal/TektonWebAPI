using AutoMapper;
using FluentAssertions;
using Moq;
using TektonWebAPI.Application.DTOs;
using TektonWebAPI.Application.Features.Products.Commands;
using TektonWebAPI.Application.Features.Products.Handlers;
using TektonWebAPI.Application.Features.Products.Queries;
using TektonWebAPI.Application.Interfaces;
using TektonWebAPI.Core.Entities;
using TektonWebAPI.Core.Interfaces;
using TektonWebAPI.Core.Utilities;

public class GetProductByIdQueryHandlerTests
{
    private readonly Mock<IProductService> _productServiceMock;
    private readonly Mock<IProductStatusCache> _productStatusCacheMock;
    private readonly Mock<IDiscountService> _discountServiceMock;
    private readonly Mock<IFinalPriceCalculator> _finalPriceCalculatorMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetProductByIdQueryHandler _handler;

    public GetProductByIdQueryHandlerTests()
    {
        _productServiceMock = new Mock<IProductService>();
        _productStatusCacheMock = new Mock<IProductStatusCache>();
        _discountServiceMock = new Mock<IDiscountService>();
        _finalPriceCalculatorMock = new Mock<IFinalPriceCalculator>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetProductByIdQueryHandler(
            _productServiceMock.Object,
            _productStatusCacheMock.Object,
            _discountServiceMock.Object,
            _finalPriceCalculatorMock.Object,
            _mapperMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnProductResponseDto_WhenProductExists()
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
        var productResponseDto = new ProductResponseDto
        {
            ProductId = productId,
            Name = "Test Product",
            StatusName = "Active",
            Stock = 10,
            Description = "Test Description",
            Price = 100.0m
        };
        var query = new GetProductByIdQuery(productId);

        _productServiceMock.Setup(service => service.GetByIdAsync(productId))
            .ReturnsAsync(Result<Product>.Success(product));
        _mapperMock.Setup(mapper => mapper.Map<ProductResponseDto>(product))
            .Returns(productResponseDto);
        _discountServiceMock.Setup(service => service.GetDiscountAsync(productId))
            .ReturnsAsync(10.0m);
        _finalPriceCalculatorMock.Setup(calculator => calculator.CalculateFinalPrice(productResponseDto))
            .Returns(90.0m);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(productResponseDto);
        result.Value!.Discount.Should().Be(10.0m);
        result.Value.FinalPrice.Should().Be(90.0m);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenProductDoesNotExist()
    {
        // Arrange
        var productId = 1;
        var query = new GetProductByIdQuery(productId);

        _productServiceMock.Setup(service => service.GetByIdAsync(productId))
            .ReturnsAsync(Result<Product>.Failure("Product not found."));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("Product not found.");
    }
}