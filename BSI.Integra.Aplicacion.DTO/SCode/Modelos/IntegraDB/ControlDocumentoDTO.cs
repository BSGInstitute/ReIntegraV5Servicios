namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ControlDocumentoDTO
    {
        public int IdControlDoc { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public bool EstadoDocumento { get; set; }
        public int IdCriterioDoc { get; set; }
        public string NombreUsuario { get; set; }

        public bool Recepcionado { get; set; }
    }
}
