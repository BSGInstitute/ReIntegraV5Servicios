using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IFaseOportunidadService
    {
        #region Metodos Base
        FaseOportunidad Add(FaseOportunidad entidad);
        FaseOportunidad Update(FaseOportunidad entidad);
        bool Delete(int id, string usuario);

        List<FaseOportunidad> Add(List<FaseOportunidad> listadoEntidad);
        List<FaseOportunidad> Update(List<FaseOportunidad> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<FaseOportunidadComboDTO> ObtenerCombo();
        IEnumerable<FaseOportunidadDTO> ObtenerFaseOportunidad();
        bool ValidarFaseCierreOportunidad(int idFase);
        bool ValidarFaseIS(int idFase);
        int ObternerFaseMaximaHistoria(int faseUno, int faseDos);
        public List<FaseOportunidadComboDTO> ObtenerFaseOportunidadTodoFiltro();
        FaseOportunidadInteraccionDTO ObtenerFaseOportunidadPorInteraccionId(int idInteraccionChat);
        OportunidadDatosChatDTO ObtenerOportunidadDatosChatPorIdFaseOportunidadPortal(string idFaseOportunidadPortal);
        OportunidadDatosChatDTO ObtenerOportunidadDatosChatPorIdFaseOportunidadPortalAA(string idFaseOportunidadPortal);

        IEnumerable<FaseOportunidadComboDTO> ObtenerComboFiltroSegmento();
    }
}
