namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProveedorCampaniaIntegraDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool PorDefecto { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }

    public class ProveedorCampaniaIntegraFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool PorDefecto { get; set; }
    }
    public class ProveedorCampaniaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool PorDefecto { get; set; }

    }
}
