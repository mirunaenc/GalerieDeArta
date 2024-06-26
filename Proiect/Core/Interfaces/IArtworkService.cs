

using Core.Dto;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IArtworkService<T> where T : class
    {
        Task<IEnumerable<Artwork>> GetAllAsync(SortingDto criterion);

        Task<Artwork> GetByIdAsync(int id);

        Task InsertAsync(Artwork artwork);

        Task UpdateAsync(int id, Artwork artwork);

        Task DeleteAsync(int id);
    }
}
