using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Core.Dto;

namespace Proiect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtworksController : ControllerBase
    {
        private readonly IRepository<Artwork> _repository;

        public ArtworksController(IRepository<Artwork> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("Get-All")]
        public async Task<IActionResult> Get([FromBody]ArtworkSortingDto criterion)
        {
            var artworks = await _repository.GetAllAsync(criterion);
            return Ok(artworks);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var artwork = await _repository.GetByIdAsync(id);
            if (artwork == null)
            {
                return NotFound();
            }

            return Ok(artwork);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] Artwork artwork)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.InsertAsync(artwork);
            return CreatedAtAction("GetById", new { id = artwork.Id }, artwork);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Artwork artwork)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artwork.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(id, artwork);
            return Ok(artwork);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var artwork = await _repository.GetByIdAsync(id);
            if (artwork == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
