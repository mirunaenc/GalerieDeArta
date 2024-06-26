using Core.Dto;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proiect.Dtos;


namespace Proiect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ArtistsController : ControllerBase
    {
        private readonly IRepository<Artist> _repository;

        public ArtistsController(IRepository<Artist> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        //[Authorize]
        [Route("Get-All")]
        public async Task<IActionResult> Get([FromBody] ArtistSortingDto criterion)
        {
            var artists = await _repository.GetAllAsync(criterion);
            return Ok(artists);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var artist = await _repository.GetByIdAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.InsertAsync(artist);
            return CreatedAtAction("GetById", new { id = artist.Id }, artist);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artist.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(id, artist);
            return Ok(artist);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> Delete(int id)
        {
            var artist = await _repository.GetByIdAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
