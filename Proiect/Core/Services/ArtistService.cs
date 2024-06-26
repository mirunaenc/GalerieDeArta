using Core.Dto;
using Core.Entities;
using Core.Interfaces;


namespace Core.Services
{
    public class ArtistService : IArtistService<Artist>
    {
        private readonly IRepository<Artist> _artistRepository;

        public ArtistService(IRepository<Artist> artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<IEnumerable<Artist>> GetAllAsync(ArtistSortingDto criterion)
        {
            return await _artistRepository.GetAllAsync(criterion);
        }

        public async Task<Artist> GetByIdAsync(int id)
        {
            return await _artistRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(Artist artist) 
        {
            await _artistRepository.InsertAsync(artist);
        }

        public async Task UpdateAsync(int id, Artist artist)
        {
            await _artistRepository.UpdateAsync(id, artist);
        }

        public async Task DeleteAsync(int id)
        {
            var artist = await _artistRepository.GetByIdAsync(id);
            if (artist != null)
            {
                await _artistRepository.DeleteAsync(id);
            }
        }

    }
}
