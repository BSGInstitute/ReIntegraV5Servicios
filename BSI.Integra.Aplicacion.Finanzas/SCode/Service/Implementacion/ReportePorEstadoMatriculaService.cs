using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ReportePorEstadoMatriculaService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReportePorEstadoMatricula
    /// </summary>
    public class ReportePorEstadoMatriculaService : IReportePorEstadoMatriculaService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ReportePorEstadoMatriculaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<TReportePorEstadoMatricula, ReportePorEstadoMatricula>(MemberList.None).ReverseMap();
                //cfg.CreateMap<ReportePorEstadoMatriculaRecibidoDTO, ReportePorEstadoMatricula>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }



        ///Autor:Griselberto
        ///Fecha: 19/01/2023
        ///<summary>
        /// Obtener Tarifario Detalle para modulo Cronograma Pagos
        /// </summary>
        /// <param name="filtro">Id Matricula Cabecera</param>
        /// <returns> Lista Tarifario Detalle Agenda: List<TarifarioDetalleAgendaDTO></returns>
        public List<ReporteMatriculadoDTO> ObtenerReporteMatriculados(filtroReportePorEstadoMatriculaDTO filtro)
        {
            try
            {
                return _unitOfWork.ReportePorEstadoMatriculaRepository.ObtenerReporteMatriculados(filtro);
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
        /// <param name="filtro">Id Matricula Cabecera</param>
        /// <returns> Lista Tarifario Detalle Agenda: List<TarifarioDetalleAgendaDTO></returns>
        public ReportePorEstadosMatriculaFinalDTO ObtenerReportePorEstadosMatricula(filtroReportePorEstadoMatriculaDTO filtro)
        {
            try
            {
                var reporte = _unitOfWork.ReportePorEstadoMatriculaRepository.ObtenerReportePorEstadosMatricula(filtro);
                var ListaPrincipal = reporte
                       .GroupBy(r => new { r.CoordinadoraCobranza, r.EstadoMatricula, r.AgrupacionMat })
                       .Select(g => new ReportePorEstadosMatriculaPrincipalDTO
                       {
                           CoordinadoraCobranza = g.Key.CoordinadoraCobranza,
                           EstadoMatricula = g.Key.EstadoMatricula,
                           AgrupacionMat = g.Key.AgrupacionMat
                       }).ToList();
                var Periodos = reporte.GroupBy(p => p.Periodo)
                                        .Select(pg => new ReportePorEstadosMatriculaDetalleDTO
                                        {
                                            Periodo = pg.Key,
                                            Detalle = pg
                                            .GroupBy(pgc => new { pgc.CoordinadoraCobranza, pgc.EstadoMatricula, pgc.AgrupacionMat })
                                            .Select(d => new PeriodoFechaVencimientoDTO
                                            {
                                                EstadoMatricula =d.Key.EstadoMatricula,
                                                AgrupacionMat = d.Key.AgrupacionMat,
                                                CoordinadoraCobranza = d.Key.CoordinadoraCobranza,
                                                Proyectado = d.Sum(r => r.TotalCuotaD),
                                                Real = d.Sum(r => r.RealPagoD),
                                                Pendiente = d.Sum(r => r.SaldoPendienteD)
                                            }).ToList()
                                        }).ToList();

                ReportePorEstadosMatriculaFinalDTO resultado = new ReportePorEstadosMatriculaFinalDTO();
                resultado.ListaPrincipal = ListaPrincipal;
                resultado.Detalle = Periodos;

                return resultado;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
