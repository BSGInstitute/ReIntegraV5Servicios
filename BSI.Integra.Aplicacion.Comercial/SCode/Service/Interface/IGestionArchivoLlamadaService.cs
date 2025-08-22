namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IGestionArchivoLlamadaService
    {
        string SubirArchivoAudioLlamada(byte[] archivo, string mimeType, string nombreArchivo, string rutaCompleta, string rutaBlob);
    }
}
