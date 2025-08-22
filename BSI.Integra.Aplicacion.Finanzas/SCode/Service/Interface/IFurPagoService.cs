using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IFurPagoService
    {
        #region Metodos Base
        FurPago Add(FurPago entidad);
        FurPago Update(FurPago entidad);
        bool Delete(int id, string usuario);

        List<FurPago> Add(List<FurPago> listadoEntidad);
        List<FurPago> Update(List<FurPago> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        int obtenerNumeroPagoByFur(int idFur);
        IEnumerable<FurPagoRealizadoDTO> ObtenerPagosRealizadosPorFur(int IdFur);
        bool InsertarFurPago(RegistrarFurPagoDTO Json);
        public bool ActualizarFurPago(RegistrarFurPagoDTO Json);
    }
}
