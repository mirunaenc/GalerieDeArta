using Core.Dto;
using Core.Entities;
using Core.Interfaces;


namespace Core.Services
{
    public class ArtworkService : IArtworkService<Artwork>
    {
        private readonly IRepository<Artwork> _artworkRepository;

        public ArtworkService(IRepository<Artwork> artworkRepository)
        {
            _artworkRepository = artworkRepository;
        }

        public async Task<IEnumerable<Artwork>> GetAllAsync(SortingDto criterion)
        {
            return await _artworkRepository.GetAllAsync(criterion);
        }

        public async Task<Artwork> GetByIdAsync(int id)
        {
            return await _artworkRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(Artwork artwork) 
        {
            await _artworkRepository.InsertAsync(artwork);
        }

        public async Task UpdateAsync(int id, Artwork artwork)
        {
            await _artworkRepository.UpdateAsync(id, artwork);
        }

        public async Task DeleteAsync(int id)
        {
            var artwork = await _artworkRepository.GetByIdAsync(id);
            if (artwork != null)
            {
                await _artworkRepository.DeleteAsync(id);
            }
        }
    }
}
