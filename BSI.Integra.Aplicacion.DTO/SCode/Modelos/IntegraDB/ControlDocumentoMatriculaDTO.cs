namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ControlDocumentoMatriculaDTO
    {
        public int IdControlDoc { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public int IdCriterioDoc { get; set; }
        public string NombreDocumento { get; set; }
        public bool EstadoDocumento { get; set; }
        public bool? Recepcionado { get; set; }
    }

}

