using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ITipoDocumentoAlumnoRepository : IGenericRepository<TTipoDocumentoAlumno>
    {
        #region Metodos Base
        TTipoDocumentoAlumno Add(TipoDocumentoAlumno entidad);
        IEnumerable<TTipoDocumentoAlumno> Add(IEnumerable<TipoDocumentoAlumno> listadoEntidad);
        TTipoDocumentoAlumno Update(TipoDocumentoAlumno entidad);
        IEnumerable<TTipoDocumentoAlumno> Update(IEnumerable<TipoDocumentoAlumno> listadoEntidad);
        bool Delete(int id, string usuario);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        TipoDocumentoAlumnoDTO ObtenerNombrePlantillaPorId(int id);
        TipoDocumentoAlumno ObtenerPorId(int id);
        IEnumerable<TipoDocumentoAlumnoDTO> Obtener();
        IEnumerable<PlantilaCertificadoConstanciaDTO> ObtenerPlantillaCertificadoConstancia();
        IEnumerable<TipoDocumentoAlumnoDetalleConfiguracionDTO> ObtenerDetalleConfiguracionCerficicado(int idTipoDocumentoAlumno);
    }
}
