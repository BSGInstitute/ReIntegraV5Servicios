using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IGrupoFiltroProgramaCriticoPgeneralService
    {
        #region Metodos Base
        GrupoFiltroProgramaCriticoPgeneral Add(GrupoFiltroProgramaCriticoPgeneral entidad);
        GrupoFiltroProgramaCriticoPgeneral Update(GrupoFiltroProgramaCriticoPgeneral entidad);
        bool Delete(int id, string usuario);

        List<GrupoFiltroProgramaCriticoPgeneral> Add(List<GrupoFiltroProgramaCriticoPgeneral> listadoEntidad);
        List<GrupoFiltroProgramaCriticoPgeneral> Update(List<GrupoFiltroProgramaCriticoPgeneral> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

      
        public bool GuardarAsociacion(AsociacionGrupoFiltroPGeneralDTO Json);
    }
}
