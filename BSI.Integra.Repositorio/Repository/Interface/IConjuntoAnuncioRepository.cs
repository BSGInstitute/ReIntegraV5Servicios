using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IConjuntoAnuncioRepository : IGenericRepository<TConjuntoAnuncio>
    {
        #region Metodos Base
        TConjuntoAnuncio Add(ConjuntoAnuncio entidad);
        TConjuntoAnuncio Update(ConjuntoAnuncio entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TConjuntoAnuncio> Add(IEnumerable<ConjuntoAnuncio> listadoEntidad);
        IEnumerable<TConjuntoAnuncio> Update(IEnumerable<ConjuntoAnuncio> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<ConjuntoAnuncioPanelDTO> ObtenerConjuntoAnuncio();
        IEnumerable<ConjuntoAnuncioPanelDTO> ListarConjuntoAnuncios(FiltroPaginadorDTO filtro);
        IEnumerable<CoonjuntoAnuncioUrl> ObtenerConjuntoAnuncioUrl(int IdProgramaGeneral);

    }
}
