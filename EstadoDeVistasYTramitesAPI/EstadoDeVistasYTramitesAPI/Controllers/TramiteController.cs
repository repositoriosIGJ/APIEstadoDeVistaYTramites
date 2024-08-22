using EstadoDeVistasyTramites.Service.Contract;
using EstadoDeVistasYTramites.Model;
using Microsoft.AspNetCore.Mvc;

namespace EstadoDeVistasYTramitesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TramiteController : ControllerBase
    {
        private readonly ITramiteService _tramiteService;



        public TramiteController(ITramiteService tramiteService)
        {
            _tramiteService = tramiteService;
        }

        [HttpGet("TramitesPorCorrelativo")]
        public async Task<ResponseDto<List<TramiteDto>>> GetTramiteByCorrelativo(long correlativo)
        {

            ResponseDto<List<TramiteDto>> rst = await _tramiteService.GetTramitesByCorrelativo(correlativo);


            return rst;
        }
    }
}
