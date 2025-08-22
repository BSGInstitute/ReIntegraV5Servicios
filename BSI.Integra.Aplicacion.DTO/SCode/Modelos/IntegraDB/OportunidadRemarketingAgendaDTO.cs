namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class OportunidadRemarketingAgendaDTO
    {
        public int Id { get; set; }
        public int? IdOportunidad { get; set; }
        public int IdAgendaTab { get; set; }
        public bool AplicaRedireccion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
