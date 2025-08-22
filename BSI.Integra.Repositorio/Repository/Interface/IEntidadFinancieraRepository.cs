using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IEntidadFinancieraRepository : IGenericRepository<TEntidadFinanciera>
    {
        #region Metodos Base
        TEntidadFinanciera Add(EntidadFinanciera entidad);
        TEntidadFinanciera Update(EntidadFinanciera entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TEntidadFinanciera> Add(IEnumerable<EntidadFinanciera> listadoEntidad);
        IEnumerable<TEntidadFinanciera> Update(IEnumerable<EntidadFinanciera> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<EntidadFinancieraDTO> ObtenerEntidadFinanciera();
        IEnumerable<EntidadFinancieraComboDTO> ObtenerCombo();
        IEnumerable<EntidadFinancieraDTO> ObtenerEntidadesFinancieras();
    }

}
