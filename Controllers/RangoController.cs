using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Foha.Models;
using Foha.Dtos;
using Foha.Repositories;
using AutoMapper;

namespace Foha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RangoController : ControllerBase
    {
        private readonly fohaContext _context;
        private readonly IMapper _mapper;
        private readonly IDataRepository<Rango> _repo;
        public RangoController(fohaContext context, IMapper mapper, IDataRepository<Rango> repo)
        {
            _context = context;
            _mapper = mapper;
            _repo = repo;
        }

        // GET: api/Rango
        [HttpGet]
        public IEnumerable<Rango> GetRango()
        {
            return _context.Rango;
        }

        // GET: api/Rango/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRango([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rango = await _context.Rango.FindAsync(id);

            if (rango == null)
            {
                return NotFound();
            }

            return Ok(rango);
        }

        // PUT: api/Rango/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRango([FromRoute] int id, [FromBody] EditRangoDto editRangoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != editRangoDto.IdRango)
            {
                return BadRequest();
            }

            var preRango = _mapper.Map<Rango>(editRangoDto);
            _repo.Update(preRango);
            await _repo.SaveAsync(preRango);

            return NoContent();

            // _context.Entry(rango).State = EntityState.Modified;

            // try
            // {
            //     await _context.SaveChangesAsync();
            // }
            // catch (DbUpdateConcurrencyException)
            // {
            //     if (!RangoExists(id))
            //     {
            //         return NotFound();
            //     }
            //     else
            //     {
            //         throw;
            //     }
            // }

        }

        // POST: api/Rango
        [HttpPost]
        public async Task<IActionResult> PostRango([FromBody] AddRangoDto addRangoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var preRango = _mapper.Map<Rango>(addRangoDto);
            _repo.Add(preRango);
            var saveRango = await _repo.SaveAsync(preRango);
            var RangoResponse = _mapper.Map<RangoResponseDto>(saveRango);

            return StatusCode(201, new { RangoResponse });

            // _context.Rango.Add(rango);
            // try
            // {
            //     await _context.SaveChangesAsync();
            // }
            // catch (DbUpdateException)
            // {
            //     if (RangoExists(rango.IdRango))
            //     {
            //         return new StatusCodeResult(StatusCodes.Status409Conflict);
            //     }
            //     else
            //     {
            //         throw;
            //     }
            // }

            // return CreatedAtAction("GetRango", new { id = rango.IdRango }, rango);
        }

        // DELETE: api/Rango/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRango([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rango = await _context.Rango.FindAsync(id);
            if (rango == null)
            {
                return NotFound();
            }

            _context.Rango.Remove(rango);
            await _context.SaveChangesAsync();

            return Ok(rango);
        }

        private bool RangoExists(int id)
        {
            return _context.Rango.Any(e => e.IdRango == id);
        }
    }
}