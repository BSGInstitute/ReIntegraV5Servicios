namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ProyectoAplicacionPorMatriculaCabeceraDTO
    {
        public string NombreArchivo { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public DateTime? FechaCalificacion { get; set; }
        public decimal? Nota { get; set; }
        public string ComentarioDocente { get; set; }
        public int? Version { get; set; }
    }
}
