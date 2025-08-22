using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IEmpresaAutorizadaRepository : IGenericRepository<TEmpresaAutorizadum>
    {
        #region Metodos Base
        TEmpresaAutorizadum Add(EmpresaAutorizada entidad);
        TEmpresaAutorizadum Update(EmpresaAutorizada entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TEmpresaAutorizadum> Add(IEnumerable<EmpresaAutorizada> listadoEntidad);
        IEnumerable<TEmpresaAutorizadum> Update(IEnumerable<EmpresaAutorizada> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<EmpresaAutorizadaDTO> Obtener();
        IEnumerable<EmpresaAutorizadaComboDTO> ObtenerCombo();
        IEnumerable<EmpresaAutorizadaComboDTO> ObtenerComboPorCiudad(int IdCiudad);
    }
}