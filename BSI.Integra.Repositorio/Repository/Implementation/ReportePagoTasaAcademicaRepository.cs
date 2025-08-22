using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ReportePagoTasaAcademicaRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReportePagoTasaAcademica
    /// </summary>
    public class ReportePagoTasaAcademicaRepository : IReportePagoTasaAcademicaRepository
    {
        private IDapperRepository _dapperRepository;
        public ReportePagoTasaAcademicaRepository(IDapperRepository dapperRepository)
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
        public List<ComboConceptoDTO> ObtenercomboConcepto(string nombre)
        {
            try
            {
                var data = new List<ComboConceptoDTO>();
                var _query = "SELECT DISTINCT( LTRIM(Concepto)) as nombre  FROM pla.V_ObtenerCertificadoCronogramaPagoTarifario where LTRIM(Concepto) like CONCAT('%',@nombre,'%')";
                var respuesta = _dapperRepository.QueryDapper(_query, new { nombre });
                if (!respuesta.Contains("[]") || !respuesta.Contains("null") || !respuesta.Contains(""))
                {
                    data = JsonConvert.DeserializeObject<List<ComboConceptoDTO>>(respuesta);
                }
                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        ///Autor:Griselberto
        ///Fecha: 19/01/2023
        ///<summary>
        /// Obtener Tarifario Detalle para modulo Cronograma Pagos
        /// </summary>
        /// <param name="idMatriculaCabecera">Id Matricula Cabecera</param>
        /// <returns> Lista Tarifario Detalle Agenda: List<TarifarioDetalleAgendaDTO></returns>
        public List<ReporteTasasAcademicasDTO> ObtenerReportePagosTasasAcademicas(filtroReporteTasaAcademicaDTO filtro)
        {
            try
            {
                List<ReporteTasasAcademicasDTO> items = new List<ReporteTasasAcademicasDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ObtenerReportePagosTasasAcademicas]", new
                {
                    FechaInicioPago = filtro.FechaInicio,
                    FechaFinPago =filtro.FechaFin,
                    IdCentroCosto =filtro.IdCentroCosto,
                    IdAlumno =filtro.IdAlumno,
                    IdMatricula =filtro.IdMatricula,
                    Concepto =filtro.Concepto
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteTasasAcademicasDTO>>(query);
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
