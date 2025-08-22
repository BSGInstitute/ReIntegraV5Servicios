namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ConfiguracionDatoRemarketingTipoCategoriaOrigenDTO
    {
        public int Id { get; set; }

        public int IdConfiguracionDatoRemarketing { get; set; }


        public int IdTipoCategoriaOrigen { get; set; }

        public bool Estado { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public string UsuarioModificacion { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }



    }
}
