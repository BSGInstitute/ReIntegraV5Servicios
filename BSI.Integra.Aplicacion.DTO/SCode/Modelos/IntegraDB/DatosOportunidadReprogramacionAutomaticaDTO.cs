namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class DatosOportunidadReprogramacionAutomaticaDTO
    {
        public int IdPersonalAsignado { get; set; }
        public int IdActividadCabeceraUltima { get; set; }
        public int IdTipoDato { get; set; }
        public int IdCategoriaOrigen { get; set; }
    }
    public class DatosOportunidadReprogramacionManualOperacionesDTO
    {
        public int DiasProgramacion { get; set; }
        public DateTime? FechaMaxima { get; set; }
        public DateTime? FechaProximaCuota { get; set; }
        public string FechaProximaCuotaTexto { get; set; }
        public List<List<TimeSpan?>> PersonalHorario { get; set; }
    }
    public class DatosOportunidadReprogramacionManualOperacionesNumReprogramacionesDTO
    {
        public int Id { get; set; }
        public int NroReprogramacionesNE { get; set; }
    }
    public class DatosOportunidadReprogramacionManualOperacionesSubEstadoDTO
    {
        public int Id { get; set; }
        public string SubEstadoMatricula { get; set; }
    }
    public class DatosAlumnoDTO
    {
        public string EmailAlumno { get; set; }
        public int IdPersonal { get; set; }
        public string EmailPersonal { get; set; }
        public int IdMatriculaCabecera { get; set; }
    }
}
