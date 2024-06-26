

using Core.Dto;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IArtistService<T> where T : class
    {
        Task<IEnumerable<Artist>> GetAllAsync(ArtistSortingDto criterion);

        Task<Artist> GetByIdAsync(int id);

        Task InsertAsync(Artist artist);

        Task UpdateAsync(int id, Artist artist);

        Task DeleteAsync(int id);
    }
}
