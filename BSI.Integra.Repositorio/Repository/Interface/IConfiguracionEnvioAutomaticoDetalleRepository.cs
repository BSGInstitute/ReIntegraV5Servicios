using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IConfiguracionEnvioAutomaticoDetalleRepository : IGenericRepository<TConfiguracionEnvioAutomaticoDetalle>
    {
        #region Metodos Base
        TConfiguracionEnvioAutomaticoDetalle Add(ConfiguracionEnvioAutomaticoDetalle entidad);
        TConfiguracionEnvioAutomaticoDetalle Update(ConfiguracionEnvioAutomaticoDetalle entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TConfiguracionEnvioAutomaticoDetalle> Add(IEnumerable<ConfiguracionEnvioAutomaticoDetalle> listadoEntidad);
        IEnumerable<TConfiguracionEnvioAutomaticoDetalle> Update(IEnumerable<ConfiguracionEnvioAutomaticoDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<ConfiguracionEnvioAutomaticoDetalleDTO> ObtenerConfiguracionEnvioAutomaticoDetalle(int idConfiguracionEnvioAutomatico);
        ConfiguracionEnvioAutomaticoDetalle ObtenerPorId(int id);
        IEnumerable<ConfiguracionEnvioAutomaticoDetalle> ObtenerPorIdConfiguracionEnvioAutomatico(int idConfiguracionEnvioAutomatico);
    }
}
