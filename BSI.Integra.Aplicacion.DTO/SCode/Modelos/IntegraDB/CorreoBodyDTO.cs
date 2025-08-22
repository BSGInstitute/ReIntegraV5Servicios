namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CorreoBodyDTO
    {
        public string EmailBody { get; set; }
        public List<CorreoArchivoAdjuntoDTO> ArchivosAdjuntos { get; set; }
        public CorreoBodyDTO()
        {
            ArchivosAdjuntos = new List<CorreoArchivoAdjuntoDTO>();
        }
    }
}
