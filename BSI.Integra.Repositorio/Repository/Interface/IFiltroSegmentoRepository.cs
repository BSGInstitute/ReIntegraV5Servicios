using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB; 
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroSegmentoFolder;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFiltroSegmentoRepository : IGenericRepository<TFiltroSegmento>
    {
        #region Metodos Base
        TFiltroSegmento Add(FiltroSegmento entidad);
        TFiltroSegmento Update(FiltroSegmento entidad);

        bool Delete(int id, string usuario);
        IEnumerable<TFiltroSegmento> Add(IEnumerable<FiltroSegmento> listadoEntidad);
        IEnumerable<TFiltroSegmento> Update(IEnumerable<FiltroSegmento> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public List<FiltroDTO> ObtenerTodoFiltro();
        List<ComboDTO> ObtenerCombo();

        List<ComboDTO> ObtenerSubArea(string idAreas);
        List<ComboDTO> ObtenerProgramaSubArea(string idAreas, string idSubAreas);
        List<ComboDTO> ObtenerProgramaEspecifico(string IdProgramaGeneral);
        List<FiltroSegmentoCompuestoDTO> ObtenerResultadoFiltro(int id, int idFiltroSegmentoTipoContacto);
        List<AddresseeDTO> ObtenerAddressee();
        bool EliminarPorFiltroSegmento(int idFiltroSegmento, string nombreUsuario);
        FiltroSegmentoDTO ObtenerFiltroSegmentoDatosPorId(int id);
        List<FiltroSegmentoValorTipoDTO> ObtenerFiltroValorPorIdFiltroSegmento(int idFiltroSegmento);
        List<FiltroSegmentoDetalleDTO> ObtenerDetallePorIdFiltroSegmento(int idFiltroSegmento);
        void EjecutarFiltroTipoContactoAlumnoExAlumno(FiltroSegmentoDTO obj);
        void EjecutarFiltroTipoContactoProspecto(FiltroSegmentoDTO obj);
        List<FacebookAudienciaComboDTO> ObtenerComboFacebook();
        List<FacebookAudienciaHistorialDTO> ObtenerHistorialPorIdFiltroSegmento(int idFiltroSegmento);
        bool? InsertarFacebookAudienciaAlumno(string ListaIdAlumno, int IdFacebookAudiencia, string UsuarioCreacion);
        public Alumno ObtenerAlumnoPorId(int idAlumno);


    }
}
