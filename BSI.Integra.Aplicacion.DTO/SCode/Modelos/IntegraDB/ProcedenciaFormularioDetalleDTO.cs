namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProcedenciaFormularioDetalleDTO
    {
        public int Id { get; set; }
        public int IdProcedenciaFormulario { get; set; }
        public int IdTipoInteraccion { get; set; }
    
    }

    public class ProcedenciaFormularioDetalleInteraccionDTO
    {
        public int Id { get; set; }
        public int IdProcedenciaFormulario { get; set; }
        public int IdTipoInteraccion { get; set; }
        public string TipoInteraccion { get; set; }
        public string Canal { get; set; }
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaModificacion { get; set; }
    }
}
