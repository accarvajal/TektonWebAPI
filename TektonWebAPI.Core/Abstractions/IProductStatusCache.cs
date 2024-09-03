namespace TektonWebAPI.Core.Abstractions;

public interface IProductStatusCache
{
    Dictionary<int, string> GetProductStatuses();
}