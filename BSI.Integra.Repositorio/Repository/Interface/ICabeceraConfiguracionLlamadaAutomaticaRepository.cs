using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICabeceraConfiguracionLlamadaAutomaticaRepository : IGenericRepository<TCabeceraConfiguracionLlamadaAutomatica>
    {
        #region Metodos Base
        TCabeceraConfiguracionLlamadaAutomatica Add(CabeceraConfiguracionLlamadaAutomatica entidad);
        TCabeceraConfiguracionLlamadaAutomatica Update(CabeceraConfiguracionLlamadaAutomatica entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCabeceraConfiguracionLlamadaAutomatica> Add(IEnumerable<CabeceraConfiguracionLlamadaAutomatica> listadoEntidad);
        IEnumerable<TCabeceraConfiguracionLlamadaAutomatica> Update(IEnumerable<CabeceraConfiguracionLlamadaAutomatica> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public IEnumerable<LlamadaAutomaticaConfiguracionDTO> ObtenerCabeceraConfiguracionLlamadaAutomatica();
        IEnumerable<ComboDTO> ObtenerCombo();
        public bool GenerarRegistrosRecordatorioClases(FiltroGenerarDataLLamdaAutomaticaDTO data);
        public bool GenerarRegistrosRecordatorioWebinar(FiltroGenerarDataLLamdaAutomaticaDTO data);
        public bool GenerarRegistrosRecordatorioCuotaCronograma(FiltroGenerarDataLLamdaAutomaticaDTO data);
        public bool GenerarRegistrosRecordatorioAsistencia(FiltroGenerarDataLLamdaAutomaticaDTO data);
        public bool GenerarRegistrosRecordatorioAvanceAcademicoAO(FiltroGenerarDataLLamdaAutomaticaDTO data);
        public IEnumerable<DetalleCabeceraConfiguracionDTO> ObtenerDetalleCabeceraConfiguracionClases(int IdCabecera, List<int> IdsSesion);
        public IEnumerable<DetalleCabeceraConfiguracionDTO> ObtenerDetalleCabeceraConfiguracionWebinar(int IdCabecera, List<int> IdsSesion);
        public IEnumerable<DetalleCabeceraConfiguracionDTO> ObtenerDetalleCabeceraConfiguracionCuota(int IdCabecera);
        public IEnumerable<DetalleCabeceraConfiguracionDTO> ObtenerDetalleCabeceraConfiguracionAsistencia(int IdCabecera, List<int> IdsSesion);
        public IEnumerable<DetalleCabeceraConfiguracionDTO> ObtenerDetalleCabeceraConfiguracionAvanceAcademicoAO(int IdCabecera);

        public IEnumerable<CabecerasEnProcesoDTO> ObtenerCabecerasEnProceso();
        public bool ActualizarLlamadaHoy(string IdsDetalle);
        public DatoLlamadaDTO ObtenerDatoLlamada(int IdIvrEjecucion);
        public DetalleIvrDTO ObtenerDetalleParaIvr(string CelularAlumno);
        public RangoHoraEjecucionDialerDTO ObtenerRangoHoraEjecucionDialer(int IdIvrEjecucion);
        public bool EliminarRegistrosDetalle(string UsuarioModificacion, int IdCabeceraConfiguracion);
        public IEnumerable<CabecerasEnProcesoDTO> ObtenerCabecerasEnProcesoPorIdIvrEjecucion(int IvrEjecucion);
        public bool ActualizarProcesoCompletadoCabeceraConfiguracion(string IdsCabeceraConfiguracion);
        public IEnumerable<CabecerasEnProcesoDTO> ObtenerCabecerasSinPendientesALlamar(int IdIvr);
    }
}
