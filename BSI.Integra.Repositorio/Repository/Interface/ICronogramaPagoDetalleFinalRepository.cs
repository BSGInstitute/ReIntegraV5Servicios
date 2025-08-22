using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Linq.Expressions;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICronogramaPagoDetalleFinalRepository : IGenericRepository<TCronogramaPagoDetalleFinal>
    {
        #region Metodos Base
        TCronogramaPagoDetalleFinal Add(CronogramaPagoDetalleFinal entidad);
        TCronogramaPagoDetalleFinal Update(CronogramaPagoDetalleFinal entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCronogramaPagoDetalleFinal> Add(IEnumerable<CronogramaPagoDetalleFinal> listadoEntidad);
        IEnumerable<TCronogramaPagoDetalleFinal> Update(IEnumerable<CronogramaPagoDetalleFinal> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<CronogramaPagoDetalleFinalDTO> ObtenerCronogramaPagoDetalleFinal();
        IEnumerable<CronogramaPagoDetalleFinalComboDTO> ObtenerCombo();
        IEnumerable<CronogramaPagoDetalleFinalCuotaDTO> ObtenerListaCuotaPorIdMatriculaCabecera(int idMatriculaCabecera);
        List<CronogramaPagoDetalleFinalFinanzasDTO> ObtenerCronogramaFinanzasPorVersionYMCabecera(int version, int idMatriculaCabecera);
        IEnumerable<CronogramaPagoDetalleFinalDTO> ObtenerCronograma(int idMatriculaCabecera);
        List<ProgramaListaCuotaDTO> ObtenerListaCuotaPrograma(int idMatricula);
        List<ResultadoFechaCompromiso> ObtenerVersionesFechaCompromiso(int idCuota);
        List<AgendaAtcCompromiso> ObtenerAgendaAtcCompromiso(int idCuota);
        List<CronogramaPagoDetalleFinalDTO> ObtenerCronogramaFinanzas(int version, int idMatriculaCabecera);
        List<CuotaDataAdicionalDTO> ObtenerCuotaDataAdicional(int idMatriculaCabecera);
        public List<ListadoCuotaCrepDTO> ObtenerCuotasCrepPorCodigoMatricula(int idMatriculaCabecera, int? version);
        public List<ListadoCuotasModificadasDTO> ObtenerCuotas(int idMatriculaCabecera, int? version);
        public int ActualizarEnviado(string CodigoMatricula, int NroCuota, int NroSubCuota);
        public int ActualizarUltimo(string CodigoMatricula, int NroCuota, int NroSubCuota);
        public int PagarCuotaCDPG_CtoFinal(string idMat, int NroCuota, int NroSubCuota, DateTime FechaPago, double MontoPagado, double MoraBanco, string MonedaPago, string NroDoc, int IdPeriodo, string uaurio, ref string Excepcion, int? IdTipoComprobante, string? NroDocumentoComprobante, string? NombreRazonSocial);     
        List<MatriculaControlDocumentoDTO> ObtenerDocumentosFiltrado(FiltroControlDocumentoDTO filtro);
        public int ObtenerMaximaVersionCronograma(int idMatriculaCabecera);
        IEnumerable<DetalleCuotasTransaccionAuditoriaDTO> ObtenerDetalleCuotasTransaccionAuditoria(FiltroDetalleCuotasTransaccionAuditoriaDTO filtroDetalle);
        IEnumerable<DetalleMatriculaTransaccionAuditoriaDTO> ObtenerDetalleMatriculaTransaccionAuditoria(FiltroDetalleMatriculaTransaccionAuditoriaDTO filtroDetalle);
        bool ActualizaEnviadoSiigo(int id);
    }
}