using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IEmpresaAutorizadaService
    {
        #region Metodos Base
        EmpresaAutorizada Add(EmpresaAutorizadaDTO data, string Usuario);
        EmpresaAutorizada Update(EmpresaAutorizadaDTO data, string Usuario);
        bool Delete(int id, string usuario);

        List<EmpresaAutorizada> Add(List<EmpresaAutorizada> listadoEntidad);
        List<EmpresaAutorizada> Update(List<EmpresaAutorizada> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<EmpresaAutorizadaComboDTO> ObtenerCombo();

    }
}
