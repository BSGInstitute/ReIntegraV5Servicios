using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IComprobantePagoPorFurRepository : IGenericRepository<TComprobantePagoPorFur>
    {
        #region Metodos Base
        TComprobantePagoPorFur Add(ComprobantePagoPorFur entidad);
        TComprobantePagoPorFur Update(ComprobantePagoPorFur entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TComprobantePagoPorFur> Add(IEnumerable<ComprobantePagoPorFur> listadoEntidad);
        IEnumerable<TComprobantePagoPorFur> Update(IEnumerable<ComprobantePagoPorFur> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<ReporteEgresoPorRubroDTO> ObtenerDatosReporteEgresosPorRubro(string IdPais, DateTime FechaInicio, DateTime FechaFin);
        List<DesgloseReporteEgresoPorRubroDTO> ObtenerDesgloceReporteEgresosPorRubro(string IdEmpresa, DateTime FechaInicio, DateTime @FechaFin, int IdRubro);
        
    }
}
