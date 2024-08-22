using EstadoDeVistasyTramites.Service.Contract;
using EstadoDeVistasYTramites.Model;
using Microsoft.AspNetCore.Mvc;

namespace EstadoDeVistasYTramitesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VistaController : ControllerBase
    {
        private readonly IVistaService _vistaService;

        public VistaController(IVistaService vistaService)
        {
            _vistaService = vistaService;
        }

        [HttpGet("VistasPorCorrelativoYNroTramite")]
        public async Task<ResponseDto<List<VistaDto>>> GetVistaByCorrelativo(long correlativo, long tramite)
        {

            ResponseDto<List<VistaDto>> vistasDto = await _vistaService.GetVistasPorCorrelativoYTramite(correlativo, tramite);


            return vistasDto;
        }
    }
}
