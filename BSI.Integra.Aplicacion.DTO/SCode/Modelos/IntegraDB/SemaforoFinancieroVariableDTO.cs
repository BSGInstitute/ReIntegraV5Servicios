namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SemaforoFinancieroVariableDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool? AplicaUnidad { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class SemaforoFinancieroVariableComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Modalidad { get; set; }
        public string Codigo { get; set; }
        public bool? ConsiderarEnvioAutomatico { get; set; }
        public string TipoPersonal { get; set; }
    }
    public class SemaforoFinancieroDetalleV2DTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Mensaje { get; set; }
        public string Color { get; set; }
        public string Usuario { get; set; }
        public int Actualizar { get; set; }
        public List<SemaforoFinancieroDetalleVariableDTO> Variable { get; set; }
    }
}
