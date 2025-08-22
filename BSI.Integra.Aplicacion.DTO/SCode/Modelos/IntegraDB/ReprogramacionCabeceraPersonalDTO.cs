namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReprogramacionCabeceraPersonalDTO
    {
        public int Id { get; set; }
        public int IdActividadCabecera { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int ReproDia { get; set; }
        public DateTime FechaReprogramacion { get; set; }
        public int? IdPersonal { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
