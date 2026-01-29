using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPEspecificoSesionRepository : IGenericRepository<TPespecificoSesion>
    {
        #region Metodos Base
        TPespecificoSesion Add(PEspecificoSesion entidad);
        TPespecificoSesion Update(PEspecificoSesion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPespecificoSesion> Add(IEnumerable<PEspecificoSesion> listadoEntidad);
        IEnumerable<TPespecificoSesion> Update(IEnumerable<PEspecificoSesion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PEspecificoSesion? ObtenerPorId(int id);
        IEnumerable<PEspecificoSesion> ObtenerPorIds(IEnumerable<int> ids);
        string ObtenerUrlUbicacionCiudad(int id);
        string ObtenerDireccionDictadoClases(int id);
        string ObtenerNombreCiudadDictadoClases(int id);
        string ObtenerNombreDocenteDictadoClases(int id);
        string ObtenerHorarioSemanaSesionWebex(int idPespecifico);
        List<PEspecificoSesionRecuperacionDTO> ObtenerSesionesPorPEspecifico(int idPespecifico, int idMatriculaCabecera);
        List<PEspecificoSesionFechaHoraInicioDTO> ObtenerFechaHoraInicioPorIdsPEspecificoPadre(List<int> idsPEspecificoPadre);
        Task<List<PEspecificoSesionFechaHoraInicioDTO>> ObtenerFechaHoraInicioPorIdsPEspecificoPadreAsync(List<int> idsPEspecificoPadre);
        List<PEspecificoSesionFechaHoraInicioDTO> ObtenerFechaHoraInicioPorIdsPEspecifico(List<int> idsPEspecifico);
        Task<List<PEspecificoSesionFechaHoraInicioDTO>> ObtenerFechaHoraInicioPorIdsPEspecificoAsync(List<int> idsPEspecifico);
        List<PEspecificoSesionFechaHoraInicioDTO> ObtenerFechaHoraInicioSinSesionPorIdsPEspecifico(List<int> idsPEspecifico);
        Task<List<PEspecificoSesionFechaHoraInicioDTO>> ObtenerFechaHoraInicioSinSesionPorIdsPEspecificoAsync(List<int> idsPEspecifico);
        IEnumerable<InformacionProgramaEspecificoSesionDTO> ObtenerInformacionProgramaEspecificoSesion(int idPespecifico);
        IEnumerable<PEspecificoCronogramaGrupalDTO> ObtenerSesionesPorPEspecificoGrupo(int idPespecifico, int grupo);
        List<PEspecificoSesionGrupoAnteriorDTO> ObtenerSesionesPorPEspecificoGrupoAnterior(int idPespecifico, int grupo);
        IEnumerable<PespecificoSesionCompuestoDTO> ObtenerCronogramaIndividualPorPEspecifico(DatosProgramaEspecificoDTO programaEspecifico);
        IEnumerable<CruceSesionPEspecificoDTO> ValidarCrucesSesiones(InformacionCronogramaSesionesDTO dto, double duracion);
        bool ActualizarModalidadSesion(int idPespecifico, int grupo, int idModalidadCurso, string usuario);
        int ObtenerSesionInicial(int idPespecifico, int grupo);
        PadrePespecificoHijoCompuestoDTO? ObtenerDatosPespecificoHijoPorSesion(int idSesion);
        IEnumerable<int> ObtenerIdSesiones(int idPespecificoPadre, int numeroGrupo);
        IEnumerable<int> ObtenerIdSesionesIndividuales(int idPespecifico, int numeroGrupo);
        IEnumerable<int> ListaPespecificoSesiones(int idPespecifico);
        IEnumerable<ComboDTO> ObtenerGruposProgramaEspecificoFiltro();
        IEnumerable<DetalleSesionesAlumnosDTO> ObtenerDetalleSesionesPorAlumnosFiltrado(SesionFiltroDTO detalleSesionesFiltro);
        DetalleSesionesAlumnosDTO? ObtenerDetalleSesionAlumnoPorIdSesionYIdMatriculaCabecera(int IdSesion, int IdMatriculaCabecera);
        public List<PEspecificoSesionesRecordatorioClases> ObtenerSesionesRecordatorioClases(int IdPEspecifico, int IdTipoModalidad);
        public List<PEspecificoSesionesRecordatorioClases> ObtenerSesionesRecordatorioWebinar(int IdPEspecifico, int IdTipoModalidad);
        int ObtenerPEspecificoPorPEspecificoSesion(int IdPespecificoSesion);
        public bool NotificarAlumnosPEspecificoSesionCancelacionPortal(int IdPEspecificoSesion, string Usuario);
        public bool NotificarAlumnosPEspecificoSesionReprogramacionPortal(int idPEspecificoSesion, DateTime fechaNuevaHoraInicio, string usuario);
        bool EsWebinarPasado(int idPEspecificoSesion);
    }
}
