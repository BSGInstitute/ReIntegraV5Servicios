using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IComprobantePagoOportunidadRepository : IGenericRepository<TComprobantePagoOportunidad>
    {
        #region Metodos Base
        TComprobantePagoOportunidad Add(ComprobantePagoOportunidad entidad);
        TComprobantePagoOportunidad AddAsync(ComprobantePagoOportunidad entidad);
        TComprobantePagoOportunidad Update(ComprobantePagoOportunidad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TComprobantePagoOportunidad> Add(IEnumerable<ComprobantePagoOportunidad> listadoEntidad);
        IEnumerable<TComprobantePagoOportunidad> Update(IEnumerable<ComprobantePagoOportunidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComprobantePagoOportunidadDTO> ObtenerComprobantePagoOportunidad();
        List<ComprobantePagoAlumnoDTO> ObtenerReporteComprobanteAlumno(filtroReporteComprobanteDTO filtro);
    }
}