namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class OportunidadInformacionBoDTO
    {
        public int IdAlumno;
        public int IdClasificacionPersona;
        public List<OportunidadVentaCruzadaDTO> ListaVentaCruzada;
        public List<OportunidadHistorialDTO> ListaHistorial;
        public ProgramaGeneralPreBenCompuestoDTO ProgramaGeneralPreBen;
        public List<ActividadOportunidadDTO> ActividadesOportunidad;
    }
    public class ProgramaGeneralPreBenCompuestoDTO
    {
        public OportunidadCompetidorFinalizarActividadDTO OportunidadCompetidor { get; set; } = new OportunidadCompetidorFinalizarActividadDTO();
        public List<ProgramaGeneralPrerequisitoOportunidadDTO> ListaPreGeneral { get; set; } = new List<ProgramaGeneralPrerequisitoOportunidadDTO>();
        public List<ProgramaGeneralPrerequisitoOportunidadDTO> ListaPreEspecifico { get; set; } = new List<ProgramaGeneralPrerequisitoOportunidadDTO>();
        public List<ProgramaGeneralBeneficioOportunidadDTO> ListaBeneficios { get; set; } = new List<ProgramaGeneralBeneficioOportunidadDTO>();
        public List<ComboDTO> ListaCompetidores { get; set; } = new List<ComboDTO>();
    }
    public class ActividadOportunidadDTO
    {
        public int IdDetalle { get; set; }
        public int Numeracion { get; set; }
        public int IdOportunidad { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime? FechaReal { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Duracion { get; set; }
        public string Ocurrencia { get; set; }
        public List<OcurrenciaDTO> Ocurrencias { get; set; }
        public string Comentario { get; set; }
        public string TipoActividad { get; set; }
        public string IdActividadRemota { get; set; }
        public int? IdLlamada { get; set; }
        //public int Total { get; set; }
        public string TelefonoDestino { get; set; }
        public int? NumeroMaximoLlamadas { get; set; }
        public string Central { get; set; }
        //public int IdActividadCabecera { get; set; }
        public int IdActividadDetalle { get; set; }
        //public int? CantidadActividad { get; set; }
        public string EstadoLlamada { get; set; }
        public int? DuracionTimbrado { get; set; }
        public int? DuracionContesto { get; set; }
    }
}
