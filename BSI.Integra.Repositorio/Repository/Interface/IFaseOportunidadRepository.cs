using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFaseOportunidadRepository : IGenericRepository<TFaseOportunidad>
    {
        #region Metodos Base
        TFaseOportunidad Add(FaseOportunidad entidad);
        TFaseOportunidad Update(FaseOportunidad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TFaseOportunidad> Add(IEnumerable<FaseOportunidad> listadoEntidad);
        IEnumerable<TFaseOportunidad> Update(IEnumerable<FaseOportunidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<FaseOportunidadComboDTO> ObtenerCombo();
        Task<IEnumerable<FaseOportunidadComboDTO>> ObtenerComboAsync();
        IEnumerable<FaseOportunidadDTO> ObtenerFaseOportunidad();
        bool ValidarFaseCierreOportunidad(int idFase);
        Task<bool> ValidarFaseCierreOportunidadAsync(int idFase);
        bool ValidarFaseIS(int idFase);
        Task<bool> ValidarFaseISAsync(int idFase);
        int ObternerFaseMaximaHistoria(int faseUno, int faseDos);
        Task<int> ObternerFaseMaximaHistoriaAsync(int faseUno, int faseDos);
        public List<FaseOportunidadComboDTO> ObtenerFaseOportunidadTodoFiltro();
        FaseOportunidadInteraccionDTO ObtenerFaseOportunidadPorInteraccionId(int idInteraccionChat);
        OportunidadDatosChatDTO ObtenerOportunidadDatosChatPorIdFaseOportunidadPortal(string idFaseOportunidadPortal);
        OportunidadDatosChatDTO ObtenerOportunidadDatosChatPorIdFaseOportunidadPortalAA(string idFaseOportunidadPortal);

        IEnumerable<FaseOportunidadComboDTO> ObtenerComboFiltroSegmento();
    }
}
