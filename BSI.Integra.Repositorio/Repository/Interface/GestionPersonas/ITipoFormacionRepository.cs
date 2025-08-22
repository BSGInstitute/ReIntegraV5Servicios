using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;


namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface ITipoFormacionRepository : IGenericRepository<TTipoFormacion>
    {
        #region Metodos Base
        TTipoFormacion Add(TipoFormacion entidad);
        TTipoFormacion Update(TipoFormacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTipoFormacion> Add(IEnumerable<TipoFormacion> listadoEntidad);
        IEnumerable<TTipoFormacion> Update(IEnumerable<TipoFormacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<TipoFormacionDTO> Obtener();
        TipoFormacion? ObtenerPorId(int id);
    }
}
