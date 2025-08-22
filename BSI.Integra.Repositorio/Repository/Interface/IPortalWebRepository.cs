using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPortalWebRepository
    {
        NumeroOrdenDTO buscarNumeroOrden(string nombreServicio);
        bool actualizarNumeroOrden(NumeroOrdenDTO numeroOrden);
        bool registrarNumeroOrden(NumeroOrdenDTO numeroOrden);
        int RegistrarTransaccionAuditoriaPago(RegistroProcesoPagoDTO transaccion);
        TransaccionAuditoriaPagoRespuestaDTO ObtenerTransactionPorCelular(string numeroCelular);
    }
}
