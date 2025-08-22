using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IComprobantePagoOportunidadService
    {
        #region Metodos Base
        ComprobantePagoOportunidad Add(ComprobantePagoOportunidad entidad);
        ComprobantePagoOportunidad Update(ComprobantePagoOportunidad entidad);
        bool Delete(int id, string usuario);

        List<ComprobantePagoOportunidad> Add(List<ComprobantePagoOportunidad> listadoEntidad);
        List<ComprobantePagoOportunidad> Update(List<ComprobantePagoOportunidad> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ComprobantePagoOportunidadDTO> ObtenerComprobantePagoOportunidad();
        List<ComprobantePagoAlumnoDTO> ObtenerReporteComprobanteAlumno(filtroReporteComprobanteDTO filtro);
        
    }
}
