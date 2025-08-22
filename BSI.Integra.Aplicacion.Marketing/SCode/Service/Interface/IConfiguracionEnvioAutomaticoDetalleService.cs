using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IConfiguracionEnvioAutomaticoDetalleService
    {
        #region Metodos Base
        ConfiguracionEnvioAutomaticoDetalle Add(ConfiguracionEnvioAutomaticoDetalle entidad);
        ConfiguracionEnvioAutomaticoDetalle Update(ConfiguracionEnvioAutomaticoDetalle entidad);
        bool Delete(int id, string usuario);

        List<ConfiguracionEnvioAutomaticoDetalle> Add(List<ConfiguracionEnvioAutomaticoDetalle> listadoEntidad);
        List<ConfiguracionEnvioAutomaticoDetalle> Update(List<ConfiguracionEnvioAutomaticoDetalle> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        List<ConfiguracionEnvioAutomaticoDetalleDTO> ObtenerConfiguracionEnvioAutomaticoDetalle(int idConfiguracionEnvioAutomatico);
        ConfiguracionEnvioAutomaticoDetalle ObtenerPorId(int id);
        IEnumerable<ConfiguracionEnvioAutomaticoDetalle> ObtenerPorIdConfiguracionEnvioAutomatico(int idConfiguracionEnvioAutomatico);
    }
}
