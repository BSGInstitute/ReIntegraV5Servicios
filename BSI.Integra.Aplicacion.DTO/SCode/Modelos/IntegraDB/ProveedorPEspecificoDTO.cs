namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    /// DTO para insertar/actualizar un registro en pla.T_ProveedorPEspecifico
    public class ProveedorPEspecificoDTO
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public int IdPespecifico { get; set; }
    }

    /// DTO para mostrar en la grilla del modal (con nombres denormalizados)
    public class ProveedorPEspecificoGridDTO
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public string NombreDocente { get; set; } = null!;
        public int IdPespecifico { get; set; }
        public string NombreCurso { get; set; } = null!;
        public DateTime FechaAsignacion { get; set; }
    }

    /// DTO para la grilla principal (ObtenerActivoPEspecifico)
    public class ProveedorActivoPEspecificoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime FechaAsignacion { get; set; }
    }
}
