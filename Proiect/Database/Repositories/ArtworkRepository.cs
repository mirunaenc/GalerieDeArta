using Core;
using Core.Dto;
using Core.Entities;
using Core.Interfaces;
using Database.QueryExtension;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories
{
    public class ArtworkRepository : IRepository<Artwork>
    {
        private readonly ApplicationDbContext _context;

        public ArtworkRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Artwork>> GetAllAsync(SortingDto criterion)
        {
            return await _context.Artworks
                .Sort(criterion)
                .Where(criterion)
                .ToListAsync();
        }

        public async Task<Artwork> GetByIdAsync(int id)
        {
            var artwork = await _context.Artworks.FirstOrDefaultAsync(a => a.Id == id);
            if (artwork == null)
            {
                throw new KeyNotFoundException("Artwork not found.");
            }
            return artwork;
        }

        public async Task<Artwork> InsertAsync(Artwork artwork)
        {
            if (artwork == null)
            {
                throw new ArgumentNullException(nameof(artwork), "Provided artwork cannot be null.");
            }

            _context.Artworks.Add(artwork);
            await _context.SaveChangesAsync();
            return artwork;
        }

        public async Task<Artwork> UpdateAsync(int id, Artwork artwork)
        {
            var artworkToUpdate = await _context.Artworks.FirstOrDefaultAsync(a => a.Id == id);
            if (artworkToUpdate == null)
            {
                throw new KeyNotFoundException("Artwork not found.");
            }

            artworkToUpdate.Title = artwork.Title;
            artworkToUpdate.Year = artwork.Year;
            artworkToUpdate.ArtistId = artwork.ArtistId;

            _context.Artworks.Update(artworkToUpdate);
            await _context.SaveChangesAsync();
            return artworkToUpdate;
        }

        public async Task DeleteAsync(int id)
        {
            var artworkToDelete = await _context.Artworks.FindAsync(id);
            if (artworkToDelete == null)
            {
                throw new KeyNotFoundException("Artwork not found.");
            }

            _context.Artworks.Remove(artworkToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
