using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IConfiguracionEnvioMailingDetalleService
    {
        IEnumerable<ConfiguracionEnvioMailingDetalle> GetBy(Expression<Func<TConfiguracionEnvioMailingDetalle, bool>> filter);
        ConfiguracionEnvioMailingDetalle FirstById(int id);
        ConfiguracionEnvioMailingDetalle FirstBy(Expression<Func<TConfiguracionEnvioMailingDetalle, bool>> filter);
        bool Insert(ConfiguracionEnvioMailingDetalle objetoBO);
        bool Insert(IEnumerable<ConfiguracionEnvioMailingDetalle> listadoBO);
        bool Update(ConfiguracionEnvioMailingDetalle objetoBO);
        bool Update(IEnumerable<ConfiguracionEnvioMailingDetalle> listadoBO);
        void AsignacionId(TConfiguracionEnvioMailingDetalle entidad, ConfiguracionEnvioMailingDetalle objetoBO);
        TConfiguracionEnvioMailingDetalle MapeoEntidad(ConfiguracionEnvioMailingDetalle objetoBO);
        IEnumerable<ConfiguracionEnvioMailingDetalle> GetFiltered<KProperty>(IEnumerable<Expression<Func<TConfiguracionEnvioMailingDetalle, bool>>> filters, Expression<Func<TConfiguracionEnvioMailingDetalle, KProperty>> orderBy, bool ascending);
        List<ListaAlumnoMailingDTO> ObtenerRegistrosParaEnvioPersonalizado(int IdMatriculaCabecera);
        string ObtenerContenidoPlantilla(int IdPlantilla);
        bool InsertarConfiguracionEnvioMailingDetalle(List<ConfiguracionEnvioMailingDetalle> listaConfiguracionEnvioMailingDetalle);
        bool ExisteConfiguracionEnvioMailingDetalle(int idConfiguracionEnvioMailingDetalle);
        ConfiguracionEnvioMailingDetalle BuscaConfiguracionEnvioMailingDetallePorId(int idConfiguracionEnvioMailingDetalle);
        List<ConfiguracionEnvioMailingDetalle> BuscaConfiguracionEnvioMailingDetallePorIdConfiguracionEnvioMailing(int idConfiguracionEnvioMailing, bool enviadoCorrectamente);
        bool ActualizarConfiguracionEnvioMailingDetalle(ConfiguracionEnvioMailingDetalle filtro);
    }
}
