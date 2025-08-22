using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICajaPorRendirCabeceraRepository : IGenericRepository<TCajaPorRendirCabecera>
    {
        #region Metodos Base
        TCajaPorRendirCabecera Add(CajaPorRendirCabecera entidad);
        TCajaPorRendirCabecera Update(CajaPorRendirCabecera entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCajaPorRendirCabecera> Add(IEnumerable<CajaPorRendirCabecera> listadoEntidad);
        IEnumerable<TCajaPorRendirCabecera> Update(IEnumerable<CajaPorRendirCabecera> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<CajaPorRendirCabeceraComboDTO> ObtenerComboCabeceraPR();
    }
}
