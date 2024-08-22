using EstadoDeVistasyTramites.Service.Contract;
using EstadoDeVistasYTramites.Model;
using ServiceReference;
using System.Data;
using System.Xml;

namespace EstadoDeVistasyTramites.Service.Implementation
{
    public class TramiteService : ITramiteService

    {
        ServicioSoapClient ws = new ServicioSoapClient(ServicioSoapClient.EndpointConfiguration.ServicioSoap);

        List<Tramite> tramites = new();

        List<TramiteDto> tramitesDto = new();



        public async Task<ResponseDto<List<TramiteDto>>> GetTramitesByCorrelativo(long correlativo)
        {

            ResponseDto<List<TramiteDto>> rst = new();
            rst.IsSuccess = false;
            rst.Data = null;

            try
            {

                var tramitesPorCorrelativo = await ws.getTramitesXEntidadAsync(correlativo);

                XmlElement xml = tramitesPorCorrelativo.Any1;

                if (xml.InnerXml is not "")
                {
                    DataTable tramites = HelperService.ConvertXmlElementToDataTable(xml, "Tramites");

                    var listTramites = HelperService.ConvertDataTableToList<Tramite>(tramites);

                    tramitesDto = await MapTramitesToTramiteDto(listTramites);

                    rst.IsSuccess = true;
                    rst.Data = tramitesDto;
                    rst.Message = "Ok";
                }
                else
                {
                    rst.Message = "la sociedad seleccionada no tiene tramites";
                }
            }
            catch (Exception ex)
            {

                rst.Message = ex.Message;

            }

            return rst;
        }

        public async Task<List<TramiteDto>> MapTramitesToTramiteDto(List<Tramite> tramites)
        {

            foreach (var tramite in tramites)
            {
                TramiteDto dto = new TramiteDto
                {
                    Numero = tramite.DTRNROTRAM,
                    Descripcion = tramite.CDTDESCTRAM,
                    DestinoActual = tramite.DESTINO_FINAL,
                    FechaInicioTramite = tramite.DTRFECHACT,
                    FechaDestinoActual = tramite.DTRFECHART,
                    TieneVista = ParseStringToBooleano(tramite._x0031_)


                };

                tramitesDto.Add(dto);
            }

            return tramitesDto;
        }

        public bool ParseStringToBooleano(string valor)
        {

            if (valor == "0")
            {
                return false;
            }

            else
            {
                return true;
            }

        }
    }
}
