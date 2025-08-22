namespace BSI.Integra.Aplicacion.Servicios.Service.Interface
{
    public interface ITMK_TwilioService
    {
        string EnviarMensajeTexto(string mensaje, string numeroDestino);
    }
}
