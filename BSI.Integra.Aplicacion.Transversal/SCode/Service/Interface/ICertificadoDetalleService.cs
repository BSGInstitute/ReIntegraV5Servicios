namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICertificadoDetalleService
    {
        string GuardarArchivoCertificado(byte[] archivo, string tipo, string nombreArchivo);
        string GuardarArchivoCertificadoFisico(byte[] archivo, string tipo, string nombreArchivo);
    }
}
