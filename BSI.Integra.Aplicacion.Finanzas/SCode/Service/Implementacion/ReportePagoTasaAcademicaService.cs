using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ReportePagoTasaAcademicaService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReportePagoTasaAcademica
    /// </summary>
    public class ReportePagoTasaAcademicaService : IReportePagoTasaAcademicaService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ReportePagoTasaAcademicaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<TReportePagoTasaAcademica, ReportePagoTasaAcademica>(MemberList.None).ReverseMap();
                //cfg.CreateMap<ReportePagoTasaAcademicaRecibidoDTO, ReportePagoTasaAcademica>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
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
                return _unitOfWork.ReportePagoTasaAcademicaRepository.ObtenercomboConcepto(nombre);
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
                return _unitOfWork.ReportePagoTasaAcademicaRepository.ObtenerReportePagosTasasAcademicas(filtro);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
