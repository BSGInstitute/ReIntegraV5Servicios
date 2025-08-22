using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IIdiomaRepository : IGenericRepository<TIdioma>
    {
        #region Metodos Base
        TIdioma Add(Idioma entidad);
        TIdioma Update(Idioma entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TIdioma> Add(IEnumerable<Idioma> listadoEntidad);
        IEnumerable<TIdioma> Update(IEnumerable<Idioma> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        public IEnumerable<IdiomaNivelComboDTO> ObtenerIdiomaNivelCombo();

    }
}
