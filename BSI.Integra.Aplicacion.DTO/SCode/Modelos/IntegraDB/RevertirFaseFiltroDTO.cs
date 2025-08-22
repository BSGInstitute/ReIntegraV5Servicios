namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class RevertirFaseFiltroDTO
    {
        public int Id { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public string? Oportunidad { get; set; }
        public string? Asesor { get; set; }
        public string? Alumno { get; set; }
        public string? FaseOportunidad { get; set; }
        public string? TipoDato { get; set; }
        public string? Origen { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdAlumno { get; set; }
    }
}
