using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IGrupoFiltroProgramaCriticoPorAsesorService
    {
        #region Metodos Base
        GrupoFiltroProgramaCriticoPorAsesor Add(GrupoFiltroProgramaCriticoPorAsesor entidad);
        GrupoFiltroProgramaCriticoPorAsesor Update(GrupoFiltroProgramaCriticoPorAsesor entidad);
        bool Delete(int id, string usuario);

        List<GrupoFiltroProgramaCriticoPorAsesor> Add(List<GrupoFiltroProgramaCriticoPorAsesor> listadoEntidad);
        List<GrupoFiltroProgramaCriticoPorAsesor> Update(List<GrupoFiltroProgramaCriticoPorAsesor> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
