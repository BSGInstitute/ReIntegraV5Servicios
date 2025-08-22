using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface ICajaEgresoService
    {
        #region Metodos Base
        CajaEgreso Add(CajaEgreso entidad);
        CajaEgreso Update(CajaEgreso entidad);
        bool Delete(int id, string usuario);

        List<CajaEgreso> Add(List<CajaEgreso> listadoEntidad);
        List<CajaEgreso> Update(List<CajaEgreso> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        bool DevolverSolicitudCajaEgreso(int id, string usuario);
        IEnumerable<CajaPorRendirCombosDTO> ObtenerCajaPorRendirSolicitanteREC(int idPersonalResponsable);
        bool EliminarCajaEgreso(int id, string usuario);
        CajaEgresoActualizar ActualizarRegistroEgresoCajaEnviado(CajaEgresoActualizar data);
        bool GenerarRegistroEgresoCaja(GenerarRegistroEgresoDTO Data);
        bool GenerarRegistroEgresoCajaInmediato(GenerarRegistroEgresoInmediatoDTO Data);
        decimal ObtenerMontoLimite(int IdFur);
        IEnumerable<CajaEgresoGenerarPdfDTO> ObtenerCajaEgresoAprobadoByFecha(DateTime FechaInicial, DateTime FechaFinal, int IdCaja);
        IEnumerable<byte[]> ObtenerDocumentosEgresoCaja(List<int> listaEnteros);
        public List<CajaEgresoDTO>  InsertarCajaEgreso(InsertCajaEgresoDTO RequestDTO);
        List<CajaEgresoDTO> ObtenerRegistrosCajaEgreso(int IdCajaPorRendirCabecera);
        bool ActualizarCajaEgresoEstablecerRendido(int IdPersonal, int IdCajaPorRendirCabecera);
    }
}
