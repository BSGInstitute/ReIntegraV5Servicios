using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IContadorBicLogRepository : IGenericRepository<TContadorBicLog>
    {
        #region Metodos Base
        TContadorBicLog Add(ContadorBicLog entidad);
        TContadorBicLog Update(ContadorBicLog entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TContadorBicLog> Add(IEnumerable<ContadorBicLog> listadoEntidad);
        IEnumerable<TContadorBicLog> Update(IEnumerable<ContadorBicLog> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public ContadorBicLog? ObtenerUltimoRegistroPorIdOportunidad(int idOportunidad);
        List<ContadorBicLogReporteDTO> ObtenerReporteContadorBicLog(ReporteCambioFaseFiltrosDTO filtros);
    }
}
