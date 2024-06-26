
using Core.Dto;
using Core.Entities;
using Core.Interfaces;
using Database.QueryExtension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Security.Cryptography.X509Certificates;


namespace Database.Repositories
{
    public class ArtistRepository : IRepository<Artist>
    {
        private ApplicationDbContext _context;

        public ArtistRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task DeleteAsync(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist != null)
            {
                var deleteArt = _context.Artworks.Where(x => x.ArtistId == artist.Id).ToList();
                _context.Artworks.RemoveRange(deleteArt);
                _context.Artists.Remove(artist);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Artist with id {id} not found.");
            }
        }


        public async Task<IEnumerable<Artist>> GetAllAsync(SortingDto criterion)
        {
            var artists = await _context.Artists.Include(a => a.Artworks)
                .Sort(criterion)
                .Where(criterion)
                .ToListAsync();
            return artists;
        }

        public async Task<Artist> GetByIdAsync(int id)
        {
            var artist = await _context.Artists.FirstOrDefaultAsync(a => a.Id == id);

            if (artist == null)
            {
                throw new KeyNotFoundException($"No artist found with ID {id}."); 
            }

            return artist; 
        }


        public async Task<Artist> InsertAsync(Artist artist)
        {
            if (artist == null)
            {
                throw new ArgumentNullException(nameof(artist), "Provided artist cannot be null.");
            }

            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            return artist; 
        }


        public async Task<Artist> UpdateAsync(int id, Artist artist)
        {
            var artistToUpdate = await _context.Artists.FirstOrDefaultAsync(a => a.Id == id);
            if (artistToUpdate != null)
            {
                artistToUpdate.Name = artist.Name;
                artistToUpdate.Nationality = artist.Nationality;


                await _context.SaveChangesAsync();
               
                return artistToUpdate;
            }
            else
            {
               
                throw new KeyNotFoundException($"No artist found with ID {id}.");
            }
        }

    }
}
