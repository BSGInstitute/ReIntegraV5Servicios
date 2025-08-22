using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ReportePendienteMesCoordinadoraRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReportePendienteMesCoordinadora
    /// </summary>
    public class ReportePendienteMesCoordinadoraRepository : IReportePendienteMesCoordinadoraRepository
    {
        private IDapperRepository _dapperRepository;
        public ReportePendienteMesCoordinadoraRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }


        /// Autor: Griselberto Huaman C.
        /// Fecha: 29/09/2023
        /// <summary>
        /// Obtiene el reporte de pendientes por mes coordinador
        /// </summary>
        /// <returns>ReportePendientePeriodoyCoordinadorDTO</returns>
        public List<ReportePendientePeriodoyCoordinadorSeparadoDTO> ObtenerReportePendientePeriodoyCoordinadorPorMesCoordinador(ReportePendienteMesCoordinadorFiltroDTO filtroPendiente)
        {
            try
            {
                List<ReportePendientePeriodoyCoordinadorSeparadoDTO> items = new List<ReportePendientePeriodoyCoordinadorSeparadoDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ReportePendientesPeriodoyCoordinador_CierreV5]", new {
                     FechaInicio= filtroPendiente.FechaInicio.Value,
                     FechaFin = filtroPendiente.FechaFin.Value,
                     FechaCierre = filtroPendiente.FechaCorte1,
                     FechaPagoInicial = filtroPendiente.FechaPagoInicial,
                     FechaPagoFinal = filtroPendiente.FechaPagoFinal
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]") && !query.Contains("null"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientePeriodoyCoordinadorSeparadoDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Griselberto Huaman C.
        /// Fecha: 29/09/2023
        /// <summary>
        /// Obtiene el reporte de pendientes por mes coordinador
        /// </summary>
        /// <returns>ReportePendientePeriodoyCoordinadorDTO</returns>
        public List<ReportePendientePeriodoyCoordinadorSeparadoCierreDTO> ObtenerReportePendienteCierrePorMesCoordinador(ReportePendienteMesCoordinadorFiltroDTO filtroPendiente)
        {
            try
            {
              
                List<ReportePendientePeriodoyCoordinadorSeparadoCierreDTO> items = new List<ReportePendientePeriodoyCoordinadorSeparadoCierreDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ReportePendientesPeriodoyCoordinador_Cierre_CompararV5]", new {
                    FechaInicio = filtroPendiente.FechaInicio,
                    FechaFin = filtroPendiente.FechaFin,
                    FechaCierre = filtroPendiente.FechaCorte2,
                    FechaPagoInicial = filtroPendiente.FechaPagoInicial,
                    FechaPagoFinal = filtroPendiente.FechaPagoFinal
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientePeriodoyCoordinadorSeparadoCierreDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
