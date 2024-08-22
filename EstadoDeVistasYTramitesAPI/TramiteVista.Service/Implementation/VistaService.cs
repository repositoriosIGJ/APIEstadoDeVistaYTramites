using EstadoDeVistasyTramites.Service.Contract;
using EstadoDeVistasYTramites.Model;
using ServiceReference;
using System.Data;
using System.Xml;

namespace EstadoDeVistasyTramites.Service.Implementation
{
    public class VistaService : IVistaService
    {
        ServicioSoapClient ws = new ServicioSoapClient(ServicioSoapClient.EndpointConfiguration.ServicioSoap);

        List<Vista> vistas = new();
        List<VistaDto> vistasDto = new();

        public async Task<ResponseDto<List<VistaDto>>> GetVistasPorCorrelativoYTramite(long correlativo, long numeroTramite)
        {

            ResponseDto<List<VistaDto>> rst = new();
            rst.IsSuccess = false;

            try
            {

                var vistasPorCorrelativo = await ws.getVistaXNumCorrYNumTramAsync(correlativo, numeroTramite);

                XmlElement xml = vistasPorCorrelativo.Any1;

                if (xml.InnerXml is not "")
                {
                    DataTable vistas = HelperService.ConvertXmlElementToDataTable(xml, "Table");

                    var vistaTramites = HelperService.ConvertDataTableToList<Vista>(vistas);

                    vistasDto = await MapVistasToVistasDto(vistaTramites);

                    rst.Data = vistasDto;
                    rst.IsSuccess = true;
                    rst.Message = "Ok";
                }
                else
                {

                    rst.Message = "no se encontraron vistas para el tramite seleccionado";

                }

            }
            catch (Exception ex)
            {
                rst.Message = ex.Message;
            }

            return rst;
        }

        public async Task<List<VistaDto>> MapVistasToVistasDto(List<Vista> vistas)
        {

            foreach (var vista in vistas)
            {
                VistaDto dto = new VistaDto
                {
                    Detalle = vista.VISDETALLE,
                    Inspector = vista.SUBNOMBRE,
                    InicioTramite = vista.TO_CHAR_x0028_VISFECHACT_x002C__x0027_DD_MM_RRRR_x0027__x0029_,
                    InicioVista = vista.TO_CHAR_x0028_VISFECHART_x002C__x0027_DD_MM_RRRR_x0027__x0029_,
                };

                vistasDto.Add(dto);
            }

            return vistasDto;
        }
    }
}
