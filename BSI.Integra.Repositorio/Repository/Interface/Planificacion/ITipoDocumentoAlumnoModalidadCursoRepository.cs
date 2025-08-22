using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ITipoDocumentoAlumnoModalidadCursoRepository
    {
        #region Metodos Base
        TTipoDocumentoAlumnoModalidadCurso Add(TipoDocumentoAlumnoModalidadCurso entidad);
        IEnumerable<TTipoDocumentoAlumnoModalidadCurso> Add(IEnumerable<TipoDocumentoAlumnoModalidadCurso> listadoEntidad);
        TTipoDocumentoAlumnoModalidadCurso Update(TipoDocumentoAlumnoModalidadCurso entidad);
        IEnumerable<TTipoDocumentoAlumnoModalidadCurso> Update(IEnumerable<TipoDocumentoAlumnoModalidadCurso> listadoEntidad);
        bool Delete(int id, string usuario);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ValorDTO> ObtenerIdsPorIdTipoDocumentoAlumno(int idTipoDocumentoAlumno);
        IEnumerable<int> ObtenerIdsModalidadPorIdTipoDocumentoAlumno(int idTipoDocumentoAlumno);
    }
}
