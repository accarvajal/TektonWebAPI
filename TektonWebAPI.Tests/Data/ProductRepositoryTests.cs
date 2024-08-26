using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TektonWebAPI.Core.Entities;
using TektonWebAPI.Infrastructure.Data;
using TektonWebAPI.Infrastructure.Repositories;

namespace TektonWebAPI.Tests.Data;

public class ProductRepositoryTests
{
    private readonly DbContextOptions<ProductDbContext> _options;

    public ProductRepositoryTests()
    {
        _options = new DbContextOptionsBuilder<ProductDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    private ProductDbContext CreateContext() => new ProductDbContext(_options);

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

        using (var context = CreateContext())
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
        }

        // Act
        using (var context = CreateContext())
        {
            var repository = new ProductRepository(context);
            var result = await repository.GetByIdAsync(productId);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(product);
        }
    }

    [Fact]
    public async Task AddAsync_ShouldAddProduct()
    {
        // Arrange
        var product = new Product
        {
            ProductId = 2, // Cambia el ID para evitar conflictos
            Name = "New Product",
            Status = 1,
            Stock = 10,
            Description = "New Description",
            Price = 100.0m
        };

        // Act
        using (var context = CreateContext())
        {
            var repository = new ProductRepository(context);
            var result = await repository.AddAsync(product);

            // Assert
            result.IsSuccess.Should().BeTrue();
            context.Products.Should().ContainSingle(p => p.ProductId == product.ProductId);
        }
    }

    public async Task UpdateAsync_ShouldUpdateProduct()
    {
        // Arrange
        var product = new Product
        {
            ProductId = 3, // Change the ID to avoid conflicts
            Name = "Original Product",
            Status = 1,
            Stock = 10,
            Description = "Original Description",
            Price = 100.0m
        };

        using (var context = CreateContext())
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
        }

        // Detach the entity from the previous context
        using (var context = CreateContext())
        {
            context.Entry(product).State = EntityState.Detached;
        }

        product.Name = "Updated Product";
        product.Description = "Updated Description";
        product.Price = 150.0m;

        // Act
        using (var context = CreateContext())
        {
            var repository = new ProductRepository(context);
            context.Products.Attach(product); // Attach the entity to the new context
            var result = await repository.UpdateAsync(product);

            // Assert
            result.IsSuccess.Should().BeTrue();
            var updatedProduct = context.Products.Single(p => p.ProductId == product.ProductId);
            updatedProduct.Name.Should().Be("Updated Product");
            updatedProduct.Description.Should().Be("Updated Description");
            updatedProduct.Price.Should().Be(150.0m);
        }
    }
}