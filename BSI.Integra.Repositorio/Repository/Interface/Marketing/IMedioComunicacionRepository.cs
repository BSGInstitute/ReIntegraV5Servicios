using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing
{
    public interface IMedioComunicacionRepository : IGenericRepository<TMedioComunicacion>
    {
        #region Metodos Base
        TMedioComunicacion Add(MedioComunicacion entidad);
        TMedioComunicacion Update(MedioComunicacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TMedioComunicacion> Add(IEnumerable<MedioComunicacion> listadoEntidad);
        IEnumerable<TMedioComunicacion> Update(IEnumerable<MedioComunicacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
