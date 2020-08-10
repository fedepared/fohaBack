using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Foha.Models;
using Foha.Dtos;
using AutoMapper;
using Foha.Repositories;



namespace Foha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly fohaContext _context;
        private readonly IMapper _mapper;
        private readonly IDataRepository<Empleado> _repo;
        public EmpleadoController(fohaContext context, IMapper mapper, IDataRepository<Empleado> repo)
        {
            _context = context;
            _mapper = mapper;
            _repo = repo;
        }

        // GET: api/Empleado
        [HttpGet]
        public IEnumerable<Empleado> GetEmpleado()
        {
            return _context.Empleado.Include(x=>x.Sector);
        }

        // GET: api/Empleado/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpleado([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empleado = await _context.Empleado.FindAsync(id);
            

            if (empleado == null)
            {
                return NotFound();
            }

            return Ok(empleado);
        }

        // PUT: api/Empleado/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpleado([FromRoute] string id, [FromBody] EditEmpleadoDto editEmpleadoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != editEmpleadoDto.IdEmpleado)
            {
                return BadRequest();
            }

            var preEmpleado = _mapper.Map<Empleado>(editEmpleadoDto);
            _repo.Update(preEmpleado);
            await _repo.SaveAsync(preEmpleado);


            return NoContent();
            

        }

        // POST: api/Empleado
        [HttpPost]
        public async Task<IActionResult> PostEmpleado([FromBody] AddEmpleadoDto addEmpleadoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var preEmpleado = _mapper.Map<Empleado>(addEmpleadoDto);
            
            
            if(EmpleadoExists(preEmpleado.IdEmpleado)){
                return BadRequest("Ya hay un usuario con ese legajo asignado");
            }
            else{
                _repo.Add(preEmpleado);
                var saveEmpleado = await _repo.SaveAsync(preEmpleado);
                var EmpleadoResponse = _mapper.Map<EmpleadoResponseDto>(saveEmpleado);

                return StatusCode(201, new { EmpleadoResponse });
            }

            // _context.Empleado.Add(empleado);
            // try
            // {
            //     await _context.SaveChangesAsync();
            // }
            // catch (DbUpdateException)
            // {
            //     if (EmpleadoExists(empleado.IdEmpleado))
            //     {
            //         return new StatusCodeResult(StatusCodes.Status409Conflict);
            //     }
            //     else
            //     {
            //         throw;
            //     }
            // }

            // return CreatedAtAction("GetEmpleado", new { id = empleado.IdEmpleado }, empleado);
        }

        // DELETE: api/Empleado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleado([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empleado = await _context.Empleado.FindAsync(id);
            
            if (empleado == null)
            {
                return NotFound();
            }

            // if(_context.EtapaEmpleado.Where(x => x.IdEmpleado == empleado.IdEmpleado).Count() > 0){
            //     return BadRequest("El empleado está asignado a un proceso, primero debe quitarlo del mismo");
            // }

            _context.Empleado.Remove(empleado);
            await _context.SaveChangesAsync();

            return Ok(empleado);
        }

        private bool EmpleadoExists(string id)
        {
            return _context.Empleado.Any(e => e.IdEmpleado == id);
        }

    }
}