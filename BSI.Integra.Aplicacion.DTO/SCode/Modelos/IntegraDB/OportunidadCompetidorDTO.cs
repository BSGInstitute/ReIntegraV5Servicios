namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class OportunidadCompetidorDTO
    {
        public int Id { get; set; }
        public int? IdOportunidad { get; set; }
        public string OtroBeneficio { get; set; } = null!;
        public int Respuesta { get; set; }
        public string Completado { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public List<OportunidadPrerequisitoGeneralDTO> ListaPrerequisitoGeneral;
        public List<OportunidadPrerequisitoEspecificoDTO> ListaPrerequisitoEspecifico;
        public List<OportunidadBeneficioDTO> ListaBeneficio;
        public List<DetalleOportunidadCompetidorDTO> ListaCompetidor;
    }
    public class OportunidadCompetidorComboDTO
    {
        public int Id { get; set; }
        public int? IdOportunidad { get; set; }
        public string OtroBeneficio { get; set; } = null!;
    }
    public class OportunidadCompetidorAgendaDTO
    {
        public int Id { get; set; }
        public int? IdOportunidad { get; set; }
        public string OtroBeneficio { get; set; } = null!;
        public int Respuesta { get; set; }
        public string Completado { get; set; } = null!;
    }
    public class OportunidadCompetidorFinalizarActividadDTO
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public string OtroBeneficio { get; set; }
        public int Respuesta { get; set; }
        public string Completado { get; set; }
        //public CalidadProcesamientoDTO? CalidadBO { get; set; }
    }
}
