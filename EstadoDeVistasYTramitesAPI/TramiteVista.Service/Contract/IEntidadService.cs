using EstadoDeVistasYTramites.Model;

namespace EstadoDeVistasyTramites.Service.Contract
{
    public interface IEntidadService
    {
        Task<ResponseDto<EntidadDto>> GetEntidadByCorrelativo(long correlativo);

        Task<ResponseDto<EntidadDto>> GetEntidadByTramite(long tramite);
    }
}
