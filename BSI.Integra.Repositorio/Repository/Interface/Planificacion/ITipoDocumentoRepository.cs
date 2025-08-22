using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ITipoDocumentoRepository : IGenericRepository<TTipoDocumento>
    {
        #region Metodos Base
        TTipoDocumento Add(TipoDocumento entidad);
        TTipoDocumento Update(TipoDocumento entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TTipoDocumento> Add(IEnumerable<TipoDocumento> listadoEntidad);
        IEnumerable<TTipoDocumento> Update(IEnumerable<TipoDocumento> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        TipoDocumento? ObtenerPorId(int id);
        IEnumerable<TipoDocumentoDTO> Obtener();
        Task<IEnumerable<TipoDocumentoDTO>> ObtenerAsync();
    }
}
