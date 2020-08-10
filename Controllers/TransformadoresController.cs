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
using System.Data.SqlClient;
using Foha.Controllers;




namespace Foha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransformadoresController : ControllerBase
    {
        private readonly fohaContext _context;
        private readonly IMapper _mapper;
        private readonly IDataRepository<Transformadores> _repo;
        private readonly IDataRepository<Etapa> _repoEtapa;

        public TransformadoresController(fohaContext context, IMapper mapper, IDataRepository<Transformadores> repo, IDataRepository<Etapa> repoEtapa)
        {
            _context = context;
            _mapper = mapper;
            _repo = repo;
            _repoEtapa=repoEtapa;
        }

        // GET: api/Transformadores
        [HttpGet]
        public IEnumerable<Transformadores> GetTransformadores()
        {
            return _context.Transformadores.Include(t => t.Etapa);
        }

        // GET: api/Transformadores/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransformadores([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transformadores = await _context.Transformadores.FindAsync(id);

            if (transformadores == null)
            {
                return NotFound();
            }

            return Ok(transformadores);
        }

        [HttpGet("GetDataExcel")]
        public IEnumerable<object> GetDataExcel()
        {
            return (
                    from Z in _context.Transformadores.ToList()
                    select new
                    {
                        idTransfo = Z.IdTransfo,
                        oTe = Z.OTe,
                        oPe = Z.OPe,
                        observaciones = Z.Observaciones != null ? Z.Observaciones : " ",
                        potencia = Z.Potencia,
                        
                        rangoInicio = Z.RangoInicio,
                        rangoFin = Z.RangoFin,
                        nombreCli = Z.NombreCli !=  null ? Z.NombreCli : " ",
                        etapaTransfo = (from n in _context.Etapa.Where(x => x.IdTransfo == Z.IdTransfo).ToList()
                                        select new {
                                            nombreEtapa = _context.TipoEtapa.Where(x => x.IdTipoEtapa == n.IdTipoEtapa).Count() > 0 ?  _context.TipoEtapa.Where(x => x.IdTipoEtapa == n.IdTipoEtapa).FirstOrDefault().NombreEtapa : "",
                                            dateIni = n.DateIni,
                                            dateFin = n.DateFin,
                                            tiempoParc = n.TiempoParc,
                                            tiempoFin=n.TiempoFin
                                        }

                         )
                     } ).ToList();
    }

    [HttpGet("Orden")]
    public IEnumerable<object> PostOrden()
    {

        // resultado de la base de datos
		var trafosOrdenados = _context.Transformadores.OrderBy(z=>z.Anio).ThenBy(x=>x.Mes).ThenBy(y=>y.Prioridad).Include(x=>x.IdClienteNavigation).ToList();
		// lista que se le devuelve al frontend
		List<List<Transformadores>> trafosSegmentados = new List<List<Transformadores>>();
		// la papa
		List<Transformadores> trafosPorMesAnio = new List<Transformadores>();
		Transformadores ultimoTrafoAgregado = null;
		
        foreach (Transformadores trafo in trafosOrdenados)
		{
			// si no es el primero y es de otro mes o año que el anterior entonces agrego la lista y empiezo una nueva
        
			if (ultimoTrafoAgregado != null && (ultimoTrafoAgregado.Anio != trafo.Anio || ultimoTrafoAgregado.Mes != trafo.Mes))
			{
				trafosSegmentados.Add(trafosPorMesAnio);
				trafosPorMesAnio = new List<Transformadores>();
			}

            
			trafosPorMesAnio.Add(trafo);
			ultimoTrafoAgregado = trafo;
		}
        // agregamos los ultimos que quedan colgados
		trafosSegmentados.Add(trafosPorMesAnio);

        // // mostrar por consola como uqeda
		// foreach (var sublistaTrafos in trafosSegmentados)
		// {
		// 	System.Console.WriteLine("MES: {0} - AÑO: {1}", sublistaTrafos[0].Mes, sublistaTrafos[0].Anio);
		// 	foreach (var trafo in sublistaTrafos)
		// 	{
		// 		System.Console.WriteLine("#{1} - {0} ", trafo.IdTransfo, trafo.Prioridad);
		// 	}
		// 	System.Console.WriteLine("-----------");
		// }

        return trafosSegmentados;
        

    }

    // PUT: api/Transformadores/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTransformadores([FromRoute] int id, [FromBody] EditTransformadoresDto editTransformadoresDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // if (id != editTransformadoresDto.IdTransfo)
        // {
        //     return BadRequest("Error en el Id");
        // }

        var preTransformadores = _mapper.Map<Transformadores>(editTransformadoresDto);
        _repo.Update(preTransformadores);
        return StatusCode(201,await _repo.SaveAsync(preTransformadores));




    }

    // POST: api/Transformadores
    [HttpPost]
    public async Task<IActionResult> PostTransformadores([FromBody] AddTransformadoresDto addTransformadoresDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }



        if(_context.Transformadores.Where((x=>x.OPe==addTransformadoresDto.OPe && x.OTe==addTransformadoresDto.OTe)).Count()>0){
            
            var transfoIni = _context.Transformadores.Where((x=>x.OPe==addTransformadoresDto.OPe && x.OTe==addTransformadoresDto.OTe)).ToList().Max(x=>x.RangoInicio);
            addTransformadoresDto.rangoInicio=(transfoIni)+1;
            var listIguales=_context.Transformadores.Where((x=>x.OPe==addTransformadoresDto.OPe && x.OTe==addTransformadoresDto.OTe)).ToList();
            foreach(var i in listIguales){
                i.RangoFin=transfoIni+1;
            }
            addTransformadoresDto.rangoFin=_context.Transformadores.Where((x=>x.OPe==addTransformadoresDto.OPe && x.OTe==addTransformadoresDto.OTe)).Count()+1;
        }
        else{
            addTransformadoresDto.rangoFin=1;
            addTransformadoresDto.rangoInicio=1;
        }

        var preTransformadores = _mapper.Map<Transformadores>(addTransformadoresDto);
        _repo.Add(preTransformadores);
        var saveTransformadores = await _repo.SaveAsync(preTransformadores);
        var TransformadoresResponse = _mapper.Map<TransformadoresResponseDto>(saveTransformadores);

        List<AddEtapaDto> todasLasEtapas = new List<AddEtapaDto>();
        var tipoEtapa=_context.TipoEtapa.ToList();
        foreach (var i in tipoEtapa)
        {
            if (_context.Etapa.Where(x => x.IdTransfo == TransformadoresResponse.IdTransfo && x.IdTipoEtapa == i.IdTipoEtapa).Count() == 0)
            {
            var etapa = new AddEtapaDto();
            etapa.IdTipoEtapa = i.IdTipoEtapa;
            etapa.IdTransfo = TransformadoresResponse.IdTransfo;
            todasLasEtapas.Add(etapa);
            }
        }
        
        foreach(var etapa in todasLasEtapas)
        {
            var preEtapa = _mapper.Map<Etapa>(etapa);
            _repoEtapa.Add(preEtapa);
            var saveEtapa = await _repoEtapa.SaveAsync(preEtapa);
            var etapaResponse = _mapper.Map<EtapaResponseDto>(saveEtapa);

        }

        return StatusCode(201, new { TransformadoresResponse });

    }

    // [HttpPost("postTrafos")]
    // public IEnumerable<TipoEtapa> PostTrafos(){
     
    // }

    [HttpGet("getEtapasVacias/{id}")]

    public IEnumerable<TipoEtapa> GetEtapasVacias([FromRoute]int id){
        
        // string cadena="select * from tipoEtapa where idTipoEtapa NOT IN (select idTipoEtapa from Etapa Where idTransfo=";
        // string cadena2=$"{cadena}{id})";
        // string cadena3="union select * from tipoEtapa where idTipoEtapa IN (select idTipoEtapa from Etapa where (tiempoParc!='Finalizada')  and idTransfo=";
        // string cadena4=$"{cadena2}{cadena3}{id})";
        string cadena= " SELECT * from tipoEtapa Where idTipoEtapa IN (select idTipoEtapa from etapa where idTransfo=";
        string cadena2=$"{cadena}{id} and dateIni is null)";
        string cadena3="union select * from tipoEtapa where idTipoEtapa IN (select idTipoEtapa from Etapa where (tiempoParc is not null and tiempoParc!='Finalizada') and idTransfo=";
        string cadena4=$"{cadena2}{cadena3}{id})";


        var transfo=_context.Transformadores.Find(id);
        
        List<TipoEtapa> resultadoAnterior = new List<TipoEtapa>();
        var i=0;
        int[] arrayEncapsulados = {2,3,4,5,6,7,8,9,10,11,12,13,14,16,20,21,22,23,24,25,26,27,28};
        int [] arrayDistribucion = {8,9,10,11,12,13,14};
        var petroleros = 14;
        var resultado=_context.TipoEtapa.FromSql(cadena4);
        if(transfo.IdTipoTransfo==2)
        {
            resultadoAnterior=resultado.Where(z=>z.IdTipoEtapa != arrayEncapsulados[i]).ToList();
            for(i=1;i<arrayEncapsulados.Length;i++){
                resultadoAnterior=resultadoAnterior.Where(x=>x.IdTipoEtapa!=arrayEncapsulados[i]).ToList();
        
            };
            i=0;
        }
        if(transfo.IdTipoTransfo==3){
            resultadoAnterior=resultado.Where(z=>z.IdTipoEtapa != arrayDistribucion[i]).ToList();
            for(i=1;i<arrayDistribucion.Length;i++){
                resultadoAnterior=resultadoAnterior.Where(x=>x.IdTipoEtapa!=arrayDistribucion[i]).ToList();
        
            };
            i=0;
        }
        if(transfo.IdTipoTransfo==4){
            resultadoAnterior=resultado.ToList();
        }
        if(transfo.IdTipoTransfo==5){
            resultadoAnterior=resultado.Where(z=>z.IdTipoEtapa != petroleros).ToList();
        }


        return resultadoAnterior;
        
    }
    
    [HttpGet("getEtapasPausadas/{id}")]

    public IEnumerable<Etapa> GetEtapasPausadas([FromRoute]int id){
        
        string cadena="select * from Etapa where tiempoParc!='Finalizada' and dateIni is not null and idTransfo=";
        string cadena2=$"{cadena}{id}";
        // string cadena3="union select * from tipoEtapa where idTipoEtapa IN (select idTipoEtapa from Etapa where (tiempoParc!='Finalizada')  and idTransfo=";
        // string cadena4=$"{cadena2}{cadena3}{id})";

        var resultado = _context.Etapa.FromSql(cadena2);
        return resultado;
        
    }

    [HttpPost("PostTransformadoresArr")]

    public async Task<IActionResult> PostTransformadoresArr([FromBody] AddTransformadoresDto[] addTransformadoresDto){

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        foreach(var i in addTransformadoresDto){

                await PostTransformadores(i);
        }
       
        return Ok();


    }

    [HttpGet("getTrafos")]

    public IEnumerable<Transformadores> GetTrafos(){
      var resultado =_context.Transformadores.OrderBy(z=>z.Anio).ThenBy(z=>z.Mes).ThenBy(x=>x.Prioridad)
    //   .Include(x=>x.IdClienteNavigation)
    //   .Include(t=>t.IdTipoTransfoNavigation)
      .Include(t => t.Etapa).ThenInclude(x=>x.IdColorNavigation)
      .Include(f=>f.Etapa).ThenInclude(x=>x.EtapaEmpleado)
      .ToList();

      foreach(var result in resultado){
          result.Etapa=result.Etapa.OrderBy(m=>m.IdTipoEtapa).ToList();
      }
      
        

      return resultado;

    }



    // DELETE: api/Transformadores/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransformadores([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var transformadores = await _context.Transformadores.FindAsync(id);
        if (transformadores == null)
        {
            return NotFound();
        }

        if(_context.Transformadores.Where((x=>x.OPe==transformadores.OPe && x.OTe==transformadores.OTe)).Count()>0){
            var lista = _context.Transformadores.Where((x=>x.OPe==transformadores.OPe && x.OTe==transformadores.OTe)).OrderBy(x=>x.RangoInicio).ToList();
            
            foreach(var l in lista){
                if(l.RangoInicio>transformadores.RangoInicio){
                    l.RangoInicio-=1;
                }
                l.RangoFin=_context.Transformadores.Where((x=>x.OPe==transformadores.OPe && x.OTe==transformadores.OTe)).Count()-1;
            }
        }

        var listaEtapa = _context.Etapa.Where(x=>x.IdTransfo==id).ToList();
        foreach (var l in listaEtapa){
            _context.Etapa.Remove(l);
            await _context.SaveChangesAsync();    
        }

        _context.Transformadores.Remove(transformadores);
        await _context.SaveChangesAsync();

        return Ok(transformadores);
    }

    private bool TransformadoresExists(int id)
    {
        return _context.Transformadores.Any(e => e.IdTransfo == id);
    }
}
}