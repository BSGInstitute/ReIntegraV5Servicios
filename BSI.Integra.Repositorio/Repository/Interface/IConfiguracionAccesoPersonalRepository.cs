using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IConfiguracionAccesoPersonalRepository : IGenericRepository<TConfiguracionAccesoPersonal>
    {
        #region Metodos Base
        TConfiguracionAccesoPersonal Add(ConfiguracionAccesoPersonal entidad);
        TConfiguracionAccesoPersonal Update(ConfiguracionAccesoPersonal entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TConfiguracionAccesoPersonal> Add(IEnumerable<ConfiguracionAccesoPersonal> listadoEntidad);
        IEnumerable<TConfiguracionAccesoPersonal> Update(IEnumerable<ConfiguracionAccesoPersonal> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ConfiguracionAccesoPersonalDTO> ObtenerPorIdPersonal(int idPersonal);
        ConfiguracionAccesoPersonalDTO? ObtenerPorIdPersonalIdModulo(int idPersonal, int idModulo);
        ConfiguracionAccesoPersonalDTO? ObtenerPorIdPersonalUrlModulo(int idPersonal, string urlModulo);
    }
}