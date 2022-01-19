namespace CodingTask.Infrastructure.Caching
{
    public interface ICacheStoreItem
    {
        string CacheKey { get; }
    }
}