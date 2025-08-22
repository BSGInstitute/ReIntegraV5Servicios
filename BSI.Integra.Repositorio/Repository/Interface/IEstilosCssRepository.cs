using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IEstilosCssRepository : IGenericRepository<TEstilo>
    {
        #region Metodos Base
        TEstilo Add(EstilosCss entidad);
        TEstilo Update(EstilosCss entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TEstilo> Add(IEnumerable<EstilosCss> listadoEntidad);
        IEnumerable<TEstilo> Update(IEnumerable<EstilosCss> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<EstiloCombo> ObtenerCombo();
        IEnumerable<EstilosCss> ObtenerEstilosCss();

        IEnumerable<EstiloCombo> ObtenerComboTagEstilo(int id);

    }
}
