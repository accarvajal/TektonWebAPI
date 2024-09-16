namespace TektonWebAPI.Core.Errors;

public static class ProductErrors
{
    public const string ProductAlreadyExists = "Product.AlreadyExists";
    public const string ProductMismatch = "Product.Mismatch";
    public const string ProductNotFound = "Product.NotFound";

    public static Error AlreadyExists(int id) => new(
        ProductAlreadyExists, $"The Product with Id '{id}' already exists");
    public static Error Mismatch(int key, int id) => new(
        ProductMismatch, $"Product Id '{key}' mismatch '{id}'");
    public static Error NotFound(int id) => new(
        ProductNotFound, $"The Product with Id '{id}' was not found");
}
