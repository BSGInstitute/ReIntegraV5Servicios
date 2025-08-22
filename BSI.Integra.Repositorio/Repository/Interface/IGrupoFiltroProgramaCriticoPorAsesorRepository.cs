using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IGrupoFiltroProgramaCriticoPorAsesorRepository : IGenericRepository<TGrupoFiltroProgramaCriticoPorAsesor>
    {
        #region Metodos Base
        TGrupoFiltroProgramaCriticoPorAsesor Add(GrupoFiltroProgramaCriticoPorAsesor entidad);
        TGrupoFiltroProgramaCriticoPorAsesor Update(GrupoFiltroProgramaCriticoPorAsesor entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TGrupoFiltroProgramaCriticoPorAsesor> Add(IEnumerable<GrupoFiltroProgramaCriticoPorAsesor> listadoEntidad);
        IEnumerable<TGrupoFiltroProgramaCriticoPorAsesor> Update(IEnumerable<GrupoFiltroProgramaCriticoPorAsesor> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
