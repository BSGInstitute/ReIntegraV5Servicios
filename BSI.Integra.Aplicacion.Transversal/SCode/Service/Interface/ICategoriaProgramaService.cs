using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICategoriaProgramaService
    {
        #region Metodos Base
        CategoriaPrograma Add(CategoriaPrograma entidad);
        CategoriaPrograma Update(CategoriaPrograma entidad);
        bool Delete(int id, string usuario);
        List<CategoriaPrograma> Add(List<CategoriaPrograma> listadoEntidad);
        List<CategoriaPrograma> Update(List<CategoriaPrograma> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        public IEnumerable<ComboDTO> ObtenerCombo();
    }
}
