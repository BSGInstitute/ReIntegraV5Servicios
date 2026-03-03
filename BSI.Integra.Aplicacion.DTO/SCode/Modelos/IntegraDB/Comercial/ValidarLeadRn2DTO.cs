namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial
{
    // DTO para deserializar los alumnos similares de tdb.SP_BuscarAlumnosSimilaresPorCelularOCorreo
    public class AlumnoSimilarRn2DTO
    {
        public int Id { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Celular { get; set; }
        public string Celular2 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public int? IdPais { get; set; }
    }

    // DTO interno para deserializar el resultado del SP tdb.SP_ValidacionBloqueoAutomaticoRN2
    public class ValidacionRn2SpResultDTO
    {
        public int? IdAlumno { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public int? IdPais { get; set; }
    }

    public class ValidarLeadRn2Request
    {
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public Guid? IdOportunidadActual { get; set; }
        public Guid IdAsesorActual { get; set; }
    }

    public class ValidarLeadRn2Response
    {
        public bool EstaBloqueado { get; set; }
        public string? MotivoBloqueo { get; set; }
        public Guid? IdOportunidadEnConflicto { get; set; }
        public Guid? IdAsesorEnConflicto { get; set; }
        public string? FaseEnConflicto { get; set; }
    }
}
