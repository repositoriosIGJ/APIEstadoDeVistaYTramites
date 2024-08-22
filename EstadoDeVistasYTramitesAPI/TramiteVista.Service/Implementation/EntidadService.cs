using EstadoDeVistasyTramites.Service.Contract;
using EstadoDeVistasYTramites.Model;
using ServiceReference;
using System.Data;
using System.Xml;


namespace EstadoDeVistasyTramites.Service.Implementation
{
    public class EntidadService : IEntidadService


    {

        ServicioSoapClient ws = new ServicioSoapClient(ServicioSoapClient.EndpointConfiguration.ServicioSoap);

        Entidad entidad = new Entidad();

        EntidadDto entidadDto = new EntidadDto();

        ResponseDto<EntidadDto> rst = new();

        public async Task<ResponseDto<EntidadDto>> GetEntidadByCorrelativo(long correlativo)
        {

            rst.IsSuccess = false;
            rst.Data = null;

            try
            {

                var entidadPorCorrelativo = await ws.getDatosEntidadXNumCorrAsync(correlativo);

                XmlElement xml = entidadPorCorrelativo.Any1;

                if (xml.InnerXml is not "")
                {

                    DataTable sociedad = HelperService.ConvertXmlElementToDataTable(xml, "Entidades");


                    var listEntidad = HelperService.ConvertDataTableToList<Entidad>(sociedad);



                    entidad = listEntidad.FirstOrDefault();

                    entidadDto = await MapEntidadToEntidadDto(entidad);

                    rst.Data = entidadDto;
                    rst.IsSuccess = true;
                    rst.Message = "Ok";
                }
                else
                {

                    rst.Message = "Entidad no encontrada";
                }

            }
            catch (Exception ex)
            {
                rst.Message = ex.Message;
            }


            return rst;


        }

        public async Task<ResponseDto<EntidadDto>> GetEntidadByTramite(long tramite)

        {
            rst.Data = null;
            rst.IsSuccess = false;

            try
            {
                var entidadxTramite = await ws.getEntidadesXNumTramiteAsync(tramite);

                XmlElement xml = entidadxTramite.Any1;

                if (xml.InnerText is not "")
                {

                    DataTable sociedad = HelperService.ConvertXmlElementToDataTable(xml, "Entidades");

                    var listEntidad = HelperService.ConvertDataTableToList<Entidad>(sociedad);

                    entidad = listEntidad.FirstOrDefault();

                    entidadDto = await MapEntidadToEntidadDto(entidad);

                    rst.Data = entidadDto;
                    rst.IsSuccess = true;
                    rst.Message = "Ok";


                }
                else
                {

                    rst.Message = "No se encontro ninguna sociedad con el numero de tramite seleccionado";

                }

            }
            catch (Exception ex)
            {

                rst.Message = ex.Message;
            }



            return rst;
        }

        public async Task<EntidadDto> MapEntidadToEntidadDto(Entidad entidad)
        {
            entidadDto.RazonSocial = entidad.EXPRAZONSO;
            entidadDto.Correlativo = entidad.EXPNROCORR;

            if (entidad.SUBSTR_x0028_TABCONTEN1_x002C_1_x002C_60_x0029_ == null)
            {
                entidadDto.TipoSocietario = entidad.TIPOSOC;
            }
            else
            {
                entidadDto.TipoSocietario = entidad.SUBSTR_x0028_TABCONTEN1_x002C_1_x002C_60_x0029_;
            }

            return entidadDto;
        }
    }
}
