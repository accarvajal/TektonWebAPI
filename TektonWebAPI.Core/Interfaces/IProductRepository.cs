﻿namespace TektonWebAPI.Core.Interfaces;

public interface IProductRepository
{
    Task<Result<Product>> GetByIdAsync(int productId);
    Task<Result> AddAsync(Product product);
    Task<Result> UpdateAsync(Product product);
}