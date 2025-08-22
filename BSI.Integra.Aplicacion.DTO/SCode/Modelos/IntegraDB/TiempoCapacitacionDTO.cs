namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class TiempoCapacitacionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class TiempoCapacitacionComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
    public class TiempoCapacitacionAgendaDTO
    {
        public int? IdTiempoCapacitacion { get; set; }
        public int? IdTiempoCapacitacionValidacion { get; set; }
        public List<TiempoCapacitacionComboDTO> TiemposCapacitacion { get; set; } = new List<TiempoCapacitacionComboDTO>();
    }
}
