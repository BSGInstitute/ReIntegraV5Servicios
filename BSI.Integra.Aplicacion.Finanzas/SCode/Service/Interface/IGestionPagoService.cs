using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IGestionPagoService
    {
        #region Metodos Base
        GestionPago Add(GestionPago entidad);
        GestionPago Update(GestionPago entidad);
        bool Delete(int idGestionPago, string usuario);

        List<GestionPago> Add(List<GestionPago> listadoEntidad);
        List<GestionPago> Update(List<GestionPago> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        // Consultas
        IEnumerable<GestionPagoDTO> ObtenerGestionesPago(FiltroGestionPagoDTO filtro);
        GestionPagoDTO? ObtenerGestionPagoPorId(int idGestionPago);
        GestionPagoDTO? ObtenerGestionPagoPorComprobante(int idComprobantePago);

        // Cronograma
        IEnumerable<GestionPagoCronogramaDTO> ObtenerCronogramaPorGestionPago(int idGestionPago);

        // Archivos
        IEnumerable<GestionPagoArchivoDTO> ObtenerArchivosCabecera(int idGestionPago);
        IEnumerable<GestionPagoArchivoDTO> ObtenerArchivosPorCronograma(int idGestionPagoCronograma);

        // Catálogos
        IEnumerable<ModalidadPagoDTO> ObtenerModalidadesPago();
        IEnumerable<PagoEstadoDTO> ObtenerPagoEstados();

        // Operaciones de negocio
        bool InsertarGestionPago(GestionPagoInsertarDTO dto);
        bool ActualizarGestionPago(GestionPagoActualizarDTO dto);
        bool RegistrarConformidad(GestionPagoConformidadDTO dto);
        bool LevantarObservacion(GestionPagoLevantamientoDTO dto);
        bool RegistrarPagoCuota(GestionPagoCronogramaPagoDTO dto);
        bool InsertarArchivo(int idGestionPago, GestionPagoArchivoInsertarDTO dto);
        bool EliminarArchivo(int idArchivo, string usuario);
    }
}
