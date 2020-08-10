using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Foha.Models;
using AutoMapper;
using Foha.Repositories;
using Foha.Dtos;
using System.Text;

namespace Foha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtapaController : ControllerBase
    {
        private readonly fohaContext _context;
        private readonly IMapper _mapper;
        private readonly IDataRepository<Etapa> _repo;
        private readonly IDataRepository<EtapaEmpleado> _repo2;

        public EtapaController(fohaContext context, IMapper mapper, IDataRepository<Etapa> repo,IDataRepository<EtapaEmpleado> repo2)
        {

            _context = context;
            _mapper = mapper;
            _repo = repo;
            _repo2 = repo2;
        }

        // GET: api/Etapa
        [HttpGet]
        public IEnumerable<Etapa> GetEtapa()
        {
            return _context.Etapa.OrderBy(x=>x.IdEtapa).ThenBy(x=>x.IdTipoEtapa).Include(x=>x.EtapaEmpleado);
        }

        // GET: api/Etapa/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEtapa([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var etapa = await _context.Etapa.FindAsync(id);

            if (etapa == null)
            {
                return NotFound();
            }

            return Ok(etapa);
        }


        [HttpGet("{id}/resultadoTransfo")]
        public async Task<IActionResult> getEtapasPorIdTransfo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var etapa = await _context.Etapa.Where(x=> x.IdTransfo == id).ToListAsync();

            if (etapa == null)
            {
                return NotFound();
            }

            return Ok(etapa);
        }

        [HttpPut("{id}/etapaSola")]
         public async Task<IActionResult> PutEtapaSola([FromRoute] int id, [FromBody] int idTransfo)
         {
             if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var etapa = await _context.Etapa.FindAsync(id);
            
            if (etapa == null)
            {
                return NotFound();
            }
             
            string cadena="UPDATE Etapa SET idTransfo = ";
            string cadena2=$"{cadena}{idTransfo}";
            string cadena3=" WHERE idEtapa = ";
            string cadena4=$"{cadena2}{cadena3}{etapa.IdEtapa}"; 

            var resultado = _context.Etapa.FromSql(cadena4);        
            // _context.Database.ExecuteSqlCommand("UPDATE [foha].[dbo].[Etapa] SET IdTransfo = @idTransfo WHERE IdEtapa = @idEtapa)",
            // new SqlParameter("@idEtapa", etapa.IdEtapa));
            // new SqlParameter("@idTransfo",idTransformador);

            return Ok(resultado);

         }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEtapa([FromRoute] int id, [FromBody] EditEtapaDto editEtapaDto)
        {
             if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != editEtapaDto.IdEtapa)
            {
                return BadRequest();
            }
            var preEtapa = _mapper.Map<Etapa>(editEtapaDto);
            _repo.Update(preEtapa);
            await _repo.SaveAsync(preEtapa);
            return StatusCode(201,preEtapa);
        }

        // PUT: api/Etapa/5
        [HttpPut("{id}/timer")]
        public async Task<IActionResult> PutEtapaTimer([FromRoute] int id, [FromBody] EditEtapaDto editEtapaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var preEtapa = _mapper.Map<Etapa>(editEtapaDto);
            var etapaDatosAnteriores=_context.Etapa.AsNoTracking().First(z=>z.IdEtapa==preEtapa.IdEtapa);
            
            foreach(var a in editEtapaDto.EtapaEmpleado)
            {
                var preEtapaEmpleado=_mapper.Map<EtapaEmpleado>(a);
                
                //Busco si el empleado ya había trabajado en el proceso (entra también cuando se pausa)
                if(_context.EtapaEmpleado.AsNoTracking().Where(z=>z.IdEmpleado==preEtapaEmpleado.IdEmpleado && z.IdEtapa==preEtapaEmpleado.IdEtapa).Count()>0)
                {
                    var tiempoAntes=_context.EtapaEmpleado.AsNoTracking().FirstOrDefault(z=>z.IdEmpleado==preEtapaEmpleado.IdEmpleado && z.IdEtapa==preEtapaEmpleado.IdEtapa);
                    
                    
                    if(tiempoAntes.TiempoParc=="00:00:00:00" && tiempoAntes.IsEnded==null)
                    {
                        int[] etapaDatosAnterioresTiempoAntes = Array.ConvertAll(etapaDatosAnteriores.TiempoParc.Split(':'),Int32.Parse);
                        int[] etapaDatosActualesTiempoAntes = Array.ConvertAll(preEtapa.TiempoParc.Split(':'),Int32.Parse);
                        a.TiempoParc=preEtapa.TiempoParc;
                        a.IsEnded=false;
                    }
                    //si se pausa o finaliza
                    else
                    {
                        //si se finaliza
                        if(preEtapa.DateFin.HasValue)
                        {
                            a.TiempoFin=preEtapa.TiempoFin;
                            a.IsEnded=true;
                        }
                        //si se pausa
                        else
                        {
                            string[] arrTAntesStr=tiempoAntes.TiempoParc.Split(':');
                            string[] arrTDespuesStr=preEtapaEmpleado.TiempoParc.Split(':');
                            int[] tiempoAntesArrInt= Array.ConvertAll(arrTAntesStr,Int32.Parse);
                            int[] tiempoDespuesArrInt=Array.ConvertAll(arrTDespuesStr,Int32.Parse);
                            int[] tiempoIntFinal={0,0,0,0};
                            int meLlevoUno=0;
                            string tiempoStringFinal=null;
                        
                            for(int i=3;i>-1;i--)
                            {
                                switch(i){
                                    default:
                                        tiempoIntFinal[i]=tiempoAntesArrInt[i]+tiempoDespuesArrInt[i]+meLlevoUno;
                                        meLlevoUno=tiempoIntFinal[i]/60;
                                        tiempoIntFinal[i]=tiempoIntFinal[i]%60;
                                        break;
                                    case 0:
                                            tiempoIntFinal[i]=tiempoAntesArrInt[i]+tiempoDespuesArrInt[i]+meLlevoUno;
                                            break;
                                    case 1:
                                        tiempoIntFinal[i]=tiempoAntesArrInt[i]+tiempoDespuesArrInt[i]+meLlevoUno;
                                        meLlevoUno=tiempoIntFinal[i]/24;
                                        tiempoIntFinal[i]=tiempoIntFinal[i]%24;
                                        break;
                                }
                            }

                            
                            var builder = new StringBuilder();
                            for(int j=0;j<tiempoIntFinal.Length;j++)
                            {
                                if(tiempoIntFinal[j]<10)
                                {
                                    builder.Append("0");
                                }
                                builder.Append(tiempoIntFinal[j]);
                                if(j!=3)
                                {
                                    builder.Append(":");	
                                }
                                
                                tiempoStringFinal=builder.ToString();
                            }   
                            
                            preEtapaEmpleado.TiempoParc=tiempoStringFinal;  
                        }

                    }
                    try{
                        _repo2.Update(preEtapaEmpleado);
                    }
                    catch(DbUpdateConcurrencyException){
                        throw;
                    }
                }
                else{
                    try{

                        _repo2.Add(preEtapaEmpleado);
                    }
                    catch(DbUpdateConcurrencyException)
                    {
                        throw;
                    }
                }

                await _repo2.SaveAsync(preEtapaEmpleado);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EtapaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            _repo.Update(preEtapa);

            return StatusCode(201,await _repo.SaveAsync(preEtapa));
           // return StatusCode(201,preEtapa);
        }

        //A HACER
        
        // [HttpPut("{id}/start")]
        // public async Task<IActionResult> PutEtapaInicio([FromRoute] int id, [FromBody] EditEtapaDto editEtapaDto)
        // {

        //     return StatusCode(201,await _repo.SaveAsync(preEtapa));
        // }

        // POST: api/Etapa
        [HttpPost]
        public async Task<IActionResult> PostEtapa([FromBody] AddEtapaDto addEtapaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var preEtapa = _mapper.Map<Etapa>(addEtapaDto);
            _repo.Add(preEtapa);
            var saveEtapa = await _repo.SaveAsync(preEtapa);
            var etapaResponse = _mapper.Map<EtapaResponseDto>(saveEtapa);

            return Ok(etapaResponse);

            // _context.Etapa.Add(etapa);
            // try
            // {
            //     await _context.SaveChangesAsync();
            // }
            // catch (DbUpdateException)
            // {
            //     if (EtapaExists(etapa.IdEtapa))
            //     {
            //         return new StatusCodeResult(StatusCodes.Status409Conflict);
            //     }
            //     else
            //     {
            //         throw;
            //     }
            // }

            // return CreatedAtAction("GetEtapa", new { id = etapa.IdEtapa }, etapa);
            

        }

        // DELETE: api/Etapa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEtapa([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var etapa = await _context.Etapa.FindAsync(id);

            if (etapa == null)
            {
                return NotFound();
            }

            _context.Database.ExecuteSqlCommand("UPDATE [test2].[dbo].[transformadores] SET etapa = NULL WHERE etapa IN (SELECT idEtapa FROM [test2].[dbo].etapa WHERE  IdEtapa = @idEtapa)",
            new SqlParameter("@idEtapa", etapa.IdEtapa));

             _context.Database.ExecuteSqlCommand("DELETE FROM  [test2].[dbo].etapa WHERE IdEtapa = @idEtapa",
            new SqlParameter("@idEtapa", etapa.IdEtapa));

            _context.Etapa.Remove(etapa);
            await _context.SaveChangesAsync();

            return Ok(etapa);
        }


        [HttpGet("etapasFinalizadas")]

        public IEnumerable<Etapa> GetEtapasFinalizadas()
        {
            var etapas=_context.Etapa.Where(x=>x.IsEnded==true).OrderBy(z=>z.DateFin).Include(z=>z.IdTransfoNavigation).ToList();
            return etapas;
        }
        private bool EtapaExists(int id)
        {
            return _context.Etapa.Any(e => e.IdEtapa == id);
        }
    }
}