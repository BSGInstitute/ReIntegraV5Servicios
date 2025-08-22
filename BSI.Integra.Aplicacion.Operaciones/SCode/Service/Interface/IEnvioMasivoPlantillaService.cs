using Mandrill.Models;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface IEnvioMasivoPlantillaService
    {
        List<EmailAttachment> ObtenerArchivosAdjuntos(string plantilla);
        string QuitarEtiquetasArchivosAdjuntos(string plantilla);
    }
}
