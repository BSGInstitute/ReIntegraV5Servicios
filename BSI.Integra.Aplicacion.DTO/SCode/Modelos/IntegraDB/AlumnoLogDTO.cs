namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class AlumnoLogDTO
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public string CampoActualizado { get; set; } = null!;
        public string ValorAnterior { get; set; } = null!;
        public string ValorNuevo { get; set; } = null!;
    }
    public class AlumnoLogComboDTO
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public string CampoActualizado { get; set; } = null!;
        public string ValorAnterior { get; set; } = null!;
        public string ValorNuevo { get; set; } = null!;
    }
    public class AlumnoLogAgendaDTO
    {
        public int Id { get; set; }
        public string CampoActualizado { get; set; } = null!;
        public string ValorAnterior { get; set; } = null!;
        public string ValorNuevo { get; set; } = null!;
        public string UsuarioCreacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
    }
    public class AlumnoLogAgendaFechaStringDTO
    {
        public int Id { get; set; }
        public string CampoActualizado { get; set; } = null!;
        public string ValorAnterior { get; set; } = null!;
        public string ValorNuevo { get; set; } = null!;
        public string UsuarioCreacion { get; set; } = null!;
        public string FechaCreacion { get; set; } = null!;
    }
}
