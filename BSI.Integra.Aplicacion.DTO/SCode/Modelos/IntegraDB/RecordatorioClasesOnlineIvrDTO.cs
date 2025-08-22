namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class DatoLLamadaRecordatorioClasesOnlineDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public DateTime FechaSesion { get; set; }
        public string NombrePrograma { get; set; }
        public string Alumno { get; set; }
        public string CelularAlumno { get; set; }
        public string AsistenteAcademico { get; set; }
        public string Anexo { get; set; }
        public int IdSexo { get; set; }
        public int IntentoMaximo { get; set; }
        public int Intento { get; set; }
        public bool Concluido { get; set; }
        public bool Ejecutado { get; set; }
        public bool? EjecutarIvr { get; set; } = false;
        public string? Pais { get; set; } = "";
        public int IdSesion { get; set; }
        public int? ZonaHoraria { get; set; } = 0;
        public string? HoraSesion { get; set; } = "";

    }
}
