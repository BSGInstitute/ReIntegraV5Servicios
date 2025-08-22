using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ICursoPespecificoRepository : IGenericRepository<TCursoPespecifico>
    {
        #region Metodos Base
        TCursoPespecifico Add(CursoPespecifico entidad);
        TCursoPespecifico Update(CursoPespecifico entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TCursoPespecifico> Add(IEnumerable<CursoPespecifico> listadoEntidad);
        IEnumerable<TCursoPespecifico> Update(IEnumerable<CursoPespecifico> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
