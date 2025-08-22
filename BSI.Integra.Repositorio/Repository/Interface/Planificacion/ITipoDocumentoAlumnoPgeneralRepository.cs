
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ITipoDocumentoAlumnoPgeneralRepository
    {
        #region Metodos Base
        TTipoDocumentoAlumnoPgeneral Add(TipoDocumentoAlumnoPgeneral entidad);
        IEnumerable<TTipoDocumentoAlumnoPgeneral> Add(IEnumerable<TipoDocumentoAlumnoPgeneral> listadoEntidad);
        TTipoDocumentoAlumnoPgeneral Update(TipoDocumentoAlumnoPgeneral entidad);
        IEnumerable<TTipoDocumentoAlumnoPgeneral> Update(IEnumerable<TipoDocumentoAlumnoPgeneral> listadoEntidad);
        bool Delete(int id, string usuario);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ValorDTO> ObtenerIdsPorIdTipoDocumentoAlumno(int idTipoDocumentoAlumno);
        IEnumerable<int> ObtenerIdsProgramaGeneralPorIdTipoDocumentoAlumno(int idTipoDocumentoAlumno);
    }
}
