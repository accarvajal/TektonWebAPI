namespace TektonWebAPI.Core.Interfaces;

public interface IProductStatusCache
{
    Dictionary<int, string> GetProductStatuses();
}