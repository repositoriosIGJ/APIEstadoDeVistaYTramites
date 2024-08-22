using EstadoDeVistasYTramites.Model;

namespace EstadoDeVistasyTramites.Service.Contract
{
    public interface IVistaService
    {
        Task<ResponseDto<List<VistaDto>>> GetVistasPorCorrelativoYTramite(long correlativo, long numeroTramite);
    }
}
