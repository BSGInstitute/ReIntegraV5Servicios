using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ITipoDocumentoAlumnoSubEstadoMatriculaRepository
    {
        #region Metodos Base
        TTipoDocumentoAlumnoSubEstadoMatricula Add(TipoDocumentoAlumnoSubEstadoMatricula entidad);
        IEnumerable<TTipoDocumentoAlumnoSubEstadoMatricula> Add(IEnumerable<TipoDocumentoAlumnoSubEstadoMatricula> listadoEntidad);
        TTipoDocumentoAlumnoSubEstadoMatricula Update(TipoDocumentoAlumnoSubEstadoMatricula entidad);
        IEnumerable<TTipoDocumentoAlumnoSubEstadoMatricula> Update(IEnumerable<TipoDocumentoAlumnoSubEstadoMatricula> listadoEntidad);
        bool Delete(int id, string usuario);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ValorDTO> ObtenerIdsPorIdTipoDocumentoAlumno(int idTipoDocumentoAlumno);
        IEnumerable<int> ObtenerIdsSubEstadoMatriculaPorIdTipoDocumentoAlumno(int idTipoDocumentoAlumno);
    }
}
