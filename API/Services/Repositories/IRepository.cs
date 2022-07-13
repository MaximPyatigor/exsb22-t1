namespace API.Services.Repositories
{
    public interface IRepository<TCollection>
    {
        Task<IEnumerable<TCollection>> GetAllAsync();
        Task<TCollection?> GetAsync(Guid id);
        Task InsertAsync(TCollection item);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Guid id, TCollection item);
    }
}
