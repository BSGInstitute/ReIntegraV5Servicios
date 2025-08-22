using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Operaciones;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface ILlamadaInteractivaService
    {
        (CronogramaPagoDetalleDTO, ExcepcionRegistroDTO) ListaMatriculaPagoAlumnoMatricula(int idMatriculaCabecera);
        List<MedioPagoActivoPasarelaDTO> ObtenerMedioPagoPasarelaPorMatricula(int idMatriculaCabecera);
        RespuestaRegistroPreProcesoPagoDTO PreProcesoPagoCuotaAlumno(RegistroPreProcesoPagoDTO registroPreProcesoPagoDTO, RegistroTokenDTO registroToken);
        TransaccionAuditoriaPagoRespuestaDTO ObtenerTransactionPorCelular(string numeroCelular);
        bool ValidarNumeroTarjeta(string numeroTarjeta);
        bool InsertarProcesoPagoIvr(ProcesoPagoIvrDTO procesoPagoIvr, string usuario);
    }
}
