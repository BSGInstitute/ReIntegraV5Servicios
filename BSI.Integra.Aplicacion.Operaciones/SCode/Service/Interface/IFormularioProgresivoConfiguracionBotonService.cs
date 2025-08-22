using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface IFormularioProgresivoConfiguracionBotonService
    {
        #region Metodos Base
        List<FormularioProgresivoConfiguracionBoton> Add(FormularioProgresivoConfiguracionBoton entidad);
        List<FormularioProgresivoConfiguracionBoton> Update(FormularioProgresivoConfiguracionBoton entidad);
        bool Delete(int id, string usuario);
        #endregion
        FormularioProgresivoConfiguracionBoton ObtenerPorId(int id);
        IEnumerable<FormularioProgresivoConfiguracionBoton> ObtenerPorIdFormularioProgresivo(int idFormularioProgresivo);
    }
}
