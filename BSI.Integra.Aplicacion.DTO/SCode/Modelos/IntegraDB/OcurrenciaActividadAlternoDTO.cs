namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class OcurrenciaActividadAlternoDTO
    {
        public int Id { get; set; }
        public int IdOcurrencia { get; set; }
        public int IdActividadCabecera { get; set; }
        public bool? PreProgramada { get; set; }
        public int? IdOcurrenciaActividad_Padre { get; set; }
        public bool NodoPadre { get; set; }
        public int? IdPlantilla_Speech { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdActividadCabecera_Programada { get; set; }
        public string? Roles { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class OcurrenciaActividadAlternoComboDTO
    {
        public int Id { get; set; }
        public string ActividadCabecera { get; set; } = null!;
        public string OcurrenciaAlterno { get; set; } = null!;
    }
    public class ArbolOcurenciaAlternoDTO
    {
        public int? IdOcurrenciaActividad { get; set; }
        public int IdOcurrenciaReporte { get; set; }
        public string? RequiereLlamada { get; set; }
        public int? EstadoOcurrencia { get; set; }
        public string? NombreOcurrencia { get; set; }
        public string? Color { get; set; }
        public string? Roles { get; set; }
        public string Nivel { get; set; } = null!;
        public bool TieneOcurrencias { get; set; }
        public string TieneActividades { get; set; } = null!;
        public int IdFaseOportunidad { get; set; }
        public int? IdOcurrenciaActividadPadre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdPlantillaSpeech { get; set; }
        public string NombreEstadoOcurrencia { get; set; } = null!;
        public bool CrearOportunidad { get; set; }
        public string? FaseSiguiente { get; set; }
        public int? IdPlantillaWP { get; set; }
        public int? IdPlantillaCE { get; set; }
    }
    public class OcurenciaActividadCompletoDTO
    {
        public int Id { get; set; }
        public int IdOcurrencia { get; set; }
        public string Nombre { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdActividadCabecera { get; set; }
        public int? IdPlantilla_Speech { get; set; }
        public int? IdActividadCabeceraProgramada { get; set; }
        public string Roles { get; set; }

    }
}
