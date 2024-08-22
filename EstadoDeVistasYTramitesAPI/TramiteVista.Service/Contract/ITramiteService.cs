using EstadoDeVistasYTramites.Model;

namespace EstadoDeVistasyTramites.Service.Contract
{
    public interface ITramiteService

    {
        Task<ResponseDto<List<TramiteDto>>> GetTramitesByCorrelativo(long correlativo);
    }
}
