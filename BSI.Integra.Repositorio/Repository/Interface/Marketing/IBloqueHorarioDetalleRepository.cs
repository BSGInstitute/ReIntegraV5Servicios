using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing
{
    public interface IBloqueHorarioDetalleRepository : IGenericRepository<TBloqueHorarioDetalle>
    {
        #region Metodos Base
        TBloqueHorarioDetalle Add(BloqueHorarioDetalle entidad);
        TBloqueHorarioDetalle Update(BloqueHorarioDetalle entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TBloqueHorarioDetalle> Add(IEnumerable<BloqueHorarioDetalle> listadoEntidad);
        IEnumerable<TBloqueHorarioDetalle> Update(IEnumerable<BloqueHorarioDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
