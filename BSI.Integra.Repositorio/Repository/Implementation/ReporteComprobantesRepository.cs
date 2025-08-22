using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ReporteComprobantesRepository
    /// Autor: Adriana Chipana
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReporteComprobantes
    /// </summary>
    public class ReporteComprobantesRepository : IReporteComprobantesRepository
    {
        private IDapperRepository _dapperRepository;
        public ReporteComprobantesRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }



        public List<ReporteComprobantesDTO> ObtenerReporteComprobantes(int? idTipoAsociado)
        {
            try
            {
                if (idTipoAsociado != 0)
                {
                    List<ReporteComprobantesDTO> items = new List<ReporteComprobantesDTO>();
                    var _query = string.Empty;
                    _query = "Select Id, Empresa, Sede, Area, TipoPedido, TipoDocumento, NroDoc, Proveedor, TipoComprobante, NumComprobante, MonedaComprobante, MontoTotal," +
                        "FechaEmision, MesFechaEmision, FechaVencimiento, MesFechaProgramacion, CodigoFur, MontoAsociado,MontoPendiente from fin.V_ReporteComprobantes" +
                        " where IdTipoAsociado = @idTipoAsociado AND Estado = 1 ORDER BY FechaComprobante DESC";
                    var respuestaDapper = _dapperRepository.QueryDapper(_query, new { idTipoAsociado });
                    if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                    {
                        items = JsonConvert.DeserializeObject<List<ReporteComprobantesDTO>>(respuestaDapper);
                    }

                    return items;
                }
                else
                {
                    List<ReporteComprobantesDTO> items = new List<ReporteComprobantesDTO>();
                    var _query = string.Empty;
                    _query = "Select Id, Empresa, Sede, Area, TipoPedido, TipoDocumento, NroDoc, Proveedor, TipoComprobante, NumComprobante, MonedaComprobante, MontoTotal," +
                        "FechaEmision, MesFechaEmision, FechaVencimiento, MesFechaProgramacion, CodigoFur, MontoAsociado,MontoPendiente from fin.V_ReporteComprobantes" +
                        " where Estado = 1 ORDER BY FechaComprobante DESC";
                    var respuestaDapper = _dapperRepository.QueryDapper(_query, new { idTipoAsociado });
                    if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                    {
                        items = JsonConvert.DeserializeObject<List<ReporteComprobantesDTO>>(respuestaDapper);
                    }

                    return items;
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public List<ComboDTO> ObtenerTipo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = $@"
                    SELECT 
	                    Id,
	                    Nombre
                    FROM fin.T_TipoAsociadoComprobante
                    WHERE
	                    Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
