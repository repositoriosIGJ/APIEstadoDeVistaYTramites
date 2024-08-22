using EstadoDeVistasyTramites.Service.Contract;
using EstadoDeVistasYTramites.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EstadoDeVistasYTramitesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntidadController : ControllerBase
    {
        private readonly IEntidadService _entidadService;

        public EntidadController(IEntidadService entidadService)
        {
            _entidadService = entidadService;
        }


        [HttpGet("BusquedaPorCorrelativo")]
        public async Task<ResponseDto<EntidadDto>> GetEntidadByCorrelativo(long correlativo)
        {


            var rst = await _entidadService.GetEntidadByCorrelativo(correlativo);


            return rst;
        }

        [HttpGet("BusquedaporTramite")]
        public async Task<ResponseDto<EntidadDto>> GetEntidadByTramite(long tramite)
        {

            var rst = await _entidadService.GetEntidadByTramite(tramite);


            return rst;
        }






    }
}
