using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface ITipoSangreRepository : IGenericRepository<TTipoSangre>
    {
        #region Metodos Base
        TTipoSangre Add(TipoSangre entidad);
        TTipoSangre Update(TipoSangre entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTipoSangre> Add(IEnumerable<TipoSangre> listadoEntidad);
        IEnumerable<TTipoSangre> Update(IEnumerable<TipoSangre> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<TipoSangreDTO> Obtener();
        TipoSangre? ObtenerPorId(int id);
    }
}
