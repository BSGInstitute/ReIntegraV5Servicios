using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITipoDatoRepository : IGenericRepository<TTipoDato>
    {
        #region Metodos Base
        TTipoDato Add(TipoDato entidad);
        TTipoDato Update(TipoDato entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTipoDato> Add(IEnumerable<TipoDato> listadoEntidad);
        IEnumerable<TTipoDato> Update(IEnumerable<TipoDato> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        IEnumerable<TipoDatoDTO> ObtenerTipoDato();
        IEnumerable<TipoDatoFiltroDTO> ObtenerFiltroTipoDato();
        IEnumerable<ComboDTO> CargarTipoDatoChat();
    }
}
