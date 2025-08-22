using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ReporteComisionMatriculaRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 08/06/2022
    /// <summary>
    /// Gestión general de T_ReporteComisionMatricula
    /// </summary>
    public class ReporteComisionMatriculaRepository : IReporteComisionMatriculaRepository
    {
        private IDapperRepository _dapperRepository;
        public ReporteComisionMatriculaRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }
        /// Autor : Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene combo de Subestados de seguimiento comsiones matricula
        /// </summary>
        /// <returns> List<ReporteComisionMatriculaDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerListaSubEstadosParaSeguimientoComisiones()
        {
            try
            {
                List<ComboDTO> estados = new List<ComboDTO>();
                var _query = string.Empty;

                _query = "SELECT * FROM [fin].[V_SubEstadoSeguimientoComision]";
                var estadosDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(estadosDB) && !estadosDB.Contains("[]"))
                {
                    estados = JsonConvert.DeserializeObject<List<ComboDTO>>(estadosDB);
                }
                return estados;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor : Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene reporte de Comisiones Por Matricula para Grilla
        /// </summary>
        /// <returns> List<ReporteComisionMatriculaDTO> </returns>
        public IEnumerable<ReporteSeguimientoComisionesDTO> ObtenerDatosReporteSeguimientoComisiones(FiltroReporteSeguimientoComisionesDTO filtro)
        {
            try
            {
                List<ReporteSeguimientoComisionesDTO> seguimiento = new List<ReporteSeguimientoComisionesDTO>();
                var _query = string.Empty;
                if (filtro.IdEstadoComision != 1)
                    _query = "exec [fin].[SP_EstraerRegistrosSeguimientoComisiones] '" + filtro.ListaAsesores + "', " + "'" + filtro.FechaInicio + "', " + "'" + filtro.FechaFin + "', " + filtro.IdEstadoComision;
                else
                {
                    _query = "exec [fin].[SP_ExtraerVentasComisionables] '" + filtro.ListaAsesores + "', " + "'" + filtro.FechaInicio + "', " + "'" + filtro.FechaFin + "', " + filtro.IdSubEstadoComision;
                }
                var seguimientoDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(seguimientoDB) && !seguimientoDB.Contains("[]"))
                {
                    seguimiento = JsonConvert.DeserializeObject<List<ReporteSeguimientoComisionesDTO>>(seguimientoDB);
                }
                return seguimiento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
