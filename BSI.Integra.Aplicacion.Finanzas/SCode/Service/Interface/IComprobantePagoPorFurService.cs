using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IComprobantePagoPorFurService
    {
        #region Metodos Base
        ComprobantePagoPorFur Add(ComprobantePagoPorFur entidad);
        ComprobantePagoPorFur Update(ComprobantePagoPorFur entidad);
        bool Delete(int id, string usuario);

        List<ComprobantePagoPorFur> Add(List<ComprobantePagoPorFur> listadoEntidad);
        List<ComprobantePagoPorFur> Update(List<ComprobantePagoPorFur> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        bool AsociarComprobante(AsociarComprobateDTO Comprobante);
        List<DesgloseReporteEgresoPorRubroDTO> ObtenerDesgloceReporteEgresosPorRubro(string Sedes, DateTime FechaInicio,DateTime FechaFin,int IdRubro);
    }
}
