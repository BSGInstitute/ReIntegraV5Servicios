namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ProgramaGeneralArgumentoDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool EsVisibleAgenda { get; set; }
        public bool Estado { get; set; }
    }
    public class ProgramaGeneralArgumentoModalidadDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralArgumento { get; set; }
        public int IdModalidadCurso { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
    }
    public class ProgramaGeneralArgumentoDetalleDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralArgumento { get; set; }
        public string Detalle { get; set; }
        public string InstrucionPieDetalle { get; set; }
        public bool Estado { get; set; }
    }
    public class ProgramaGeneralArgumentoDetalleMotivacionDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralArgumentoDetalle { get; set; }
        public int IdProgramaGeneralMotivacion { get; set; }
        public string NombreMotivacion { get; set; }
        public bool Estado { get; set; }
    }
}
