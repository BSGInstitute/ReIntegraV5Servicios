using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ReportePorEstadoMatriculaRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReportePorEstadoMatricula
    /// </summary>
    public class ReportePorEstadoMatriculaRepository : IReportePorEstadoMatriculaRepository
    {
        private IDapperRepository _dapperRepository;
        public ReportePorEstadoMatriculaRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

 

        ///Autor:Griselberto
        ///Fecha: 19/01/2023
        ///<summary>
        /// Obtener Tarifario Detalle para modulo Cronograma Pagos
        /// </summary>
        /// <param name="idMatriculaCabecera">Id Matricula Cabecera</param>
        /// <returns> Lista Tarifario Detalle Agenda: List<TarifarioDetalleAgendaDTO></returns>
        public List<ReporteMatriculadoDTO> ObtenerReporteMatriculados(filtroReportePorEstadoMatriculaDTO filtro)
        {
            try
            {
                List<ReporteMatriculadoDTO> items = new List<ReporteMatriculadoDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ObtenerReporteMatriculados]", new
                {
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin,
                    IdsConfiguracionPeriodo = filtro.IdsConfiguracionPeriodo,
                    IdsCoordinadora = filtro.IdsCoordinadora,
                    IdsEstados = filtro.IdsEstados
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteMatriculadoDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        ///Autor:Griselberto
        ///Fecha: 19/01/2023
        ///<summary>
        /// Obtener Tarifario Detalle para modulo Cronograma Pagos
        /// </summary>
        /// <param name="idMatriculaCabecera">Id Matricula Cabecera</param>
        /// <returns> Lista Tarifario Detalle Agenda: List<TarifarioDetalleAgendaDTO></returns>
        public List<ReportePorEstadosMatriculaDTO> ObtenerReportePorEstadosMatricula(filtroReportePorEstadoMatriculaDTO filtro)
        {
            try
            {
                List<ReportePorEstadosMatriculaDTO> items = new List<ReportePorEstadosMatriculaDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ObtenerReportePorEstadosMatricula]", new
                {
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin,
                    IdsConfiguracionPeriodo = filtro.IdsConfiguracionPeriodo,
                    IdsCoordinadora = filtro.IdsCoordinadora,
                    IdsEstados = filtro.IdsEstados
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePorEstadosMatriculaDTO>>(query);
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
