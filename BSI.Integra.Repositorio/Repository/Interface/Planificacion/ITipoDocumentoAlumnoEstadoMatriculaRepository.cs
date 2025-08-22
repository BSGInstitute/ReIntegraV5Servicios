using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ITipoDocumentoAlumnoEstadoMatriculaRepository
    {
        #region Metodos Base
        TTipoDocumentoAlumnoEstadoMatricula Add(TipoDocumentoAlumnoEstadoMatricula entidad);
        IEnumerable<TTipoDocumentoAlumnoEstadoMatricula> Add(IEnumerable<TipoDocumentoAlumnoEstadoMatricula> listadoEntidad);
        TTipoDocumentoAlumnoEstadoMatricula Update(TipoDocumentoAlumnoEstadoMatricula entidad);
        IEnumerable<TTipoDocumentoAlumnoEstadoMatricula> Update(IEnumerable<TipoDocumentoAlumnoEstadoMatricula> listadoEntidad);
        bool Delete(int id, string usuario);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ValorDTO> ObtenerIdsPorIdTipoDocumentoAlumno(int idTipoDocumentoAlumno);
        IEnumerable<int> ObtenerIdsEstadoMatriculaPorIdTipoDocumentoAlumno(int idTipoDocumentoAlumno);
    }
}
