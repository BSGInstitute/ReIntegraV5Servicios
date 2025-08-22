namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProgramaGeneralProblemaDetalleSolucionRespuestaDTO
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public int IdProgramaGeneralProblemaDetalleSolucion { get; set; }
        public bool EsSeleccionado { get; set; }
        public bool EsSolucionado { get; set; }
    }
}
