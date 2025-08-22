namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ExcepcionFrecuenciaPwDTO
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class ExcepcionFrecuenciaPGeneralDTO
    {
        public int Id { get; set; }
        public int IdPEspecifico { get; set; }
    }
}
