using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IConjuntoAnuncioService
    {
        #region Metodos Base
        ConjuntoAnuncio Add(ConjuntoAnuncioEnvioDTO entidad);
        ConjuntoAnuncio Update(ConjuntoAnuncioEnvioDTO entidad);
        bool Delete(int id, string usuario);

        List<ConjuntoAnuncio> Add(List<ConjuntoAnuncio> listadoEntidad);
        List<ConjuntoAnuncio> Update(List<ConjuntoAnuncio> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DTO.ComboDTO> ObtenerCombo();
        IEnumerable<ConjuntoAnuncioPanelDTO> ObtenerConjuntoAnuncio();

        List<ConjuntoAnuncioPanelDTO> ListarConjuntoAnuncios(FiltroPaginadorDTO filtro);
        IEnumerable<CoonjuntoAnuncioUrl> ObtenerConjuntoAnuncioUrl(int IdProgramaGeneral);

    }
}
