﻿namespace TektonWebAPI.Application.DTOs;

public record ProductResponseDto
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string StatusName { get; set; } = string.Empty;
    public int Stock { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public decimal FinalPrice { get; set; }
}