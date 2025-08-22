using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IGrupoFiltroProgramaCriticoPgeneralRepository : IGenericRepository<TGrupoFiltroProgramaCriticoPgeneral>
    {
        #region Metodos Base
        TGrupoFiltroProgramaCriticoPgeneral Add(GrupoFiltroProgramaCriticoPgeneral entidad);
        TGrupoFiltroProgramaCriticoPgeneral Update(GrupoFiltroProgramaCriticoPgeneral entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TGrupoFiltroProgramaCriticoPgeneral> Add(IEnumerable<GrupoFiltroProgramaCriticoPgeneral> listadoEntidad);
        IEnumerable<TGrupoFiltroProgramaCriticoPgeneral> Update(IEnumerable<GrupoFiltroProgramaCriticoPgeneral> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
