namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ConfiguracionDatoRemarketingCategoriaOrigenDTO
    {
        public int Id { get; set; }
        public int IdConfiguracionDatoRemarketing { get; set; }

        public int IdCategoriaOrigen { get; set; }

        public bool Estado { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public string UsuarioModificacion { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }

        public byte[] RowVersion { get; set; } = null!;

        public Guid? IdMigracion { get; set; }
    }


}
