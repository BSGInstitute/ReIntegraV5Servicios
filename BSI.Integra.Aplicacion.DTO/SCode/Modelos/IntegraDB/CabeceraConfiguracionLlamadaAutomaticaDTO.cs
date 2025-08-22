namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CabeceraConfiguracionLlamadaAutomaticaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdIvrPlantilla { get; set; }
        public int IdIvrTipoConfiguracion { get; set; }
        public int? IdPespecifico { get; set; }
        public int IdIvrEjecucion { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string CongelamientoConfiguracion { get; set; } = null!;
        public FiltroGenerarDataLLamdaAutomaticaDTO? GenerarData { get; set; }
    }

    public class LlamadaAutomaticaConfiguracionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdIvrPlantilla { get; set; }
        public string IvrPlantilla { get; set; }
        public string PGeneral { get; set; }
        public string PEspecifico { get; set; }
        public string IvrEjecucion { get; set; }
        public int IdIvrEjecucion { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public string EstadoProceso { get; set; }
        public int IdIvrTipoConfiguracion { get; set; }
        public string CongelamientoConfiguracion { get; set; }
        
    }

    public class RangoHoraEjecucionDialerDTO
    {
     
        public string IvrEjecucion { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }

    }
    public class FiltroGenerarDataLLamdaAutomaticaDTO
    {
        public string? IdsSesiones { get; set; }
        public int? IdTipoModalidad { get; set; }
        public int? IdCabeceraConfiguracion { get; set; }
        public int? IdPEspecifico { get; set; }
        public bool? IsTodosWebinar { get; set; } = false;
        public int? DiasCalculoCuota { get; set; } = 0;
        public bool? IsPorPrograma { get; set; } = false;
        public bool? Asistio { get; set; } = false;
        public bool? Justifico { get; set; } = false;
        public bool? IsTodosAsistencia { get; set; } = false;

        public bool? IsSinAvance { get; set; } = false;
        public bool? IsMasDe { get; set; } = false;
        public bool? IsMenosDe { get; set; } = false;
        public bool? IsEntre { get; set; } = false;
        public decimal? Valor1 { get; set; } = 0;
        public decimal? Valor2 { get; set; } = 0;
    }

    public class DetalleCabeceraConfiguracionDTO
    {
        public int Id { get; set; }
        public int IdSesion { get; set; }
        public int IdCabecera { get; set; }
        public string Alumno { get; set; }
        public string AsistenteAcademico { get; set; }
        public DateTime FechaSesion { get; set; }
        public bool EstadoLLamada { get; set; }
    }

    public class FiltroDetalleCabeceraConfiguracionDTO
    {
        public int IdCabecera { get; set; }
        public List<int> IdsSesion { get; set; }
    }

    public class CabecerasEnProcesoDTO
    {
        public int Id { get; set; }
        public int IdIvrTipoConfiguracion { get; set; }
        public string CongelamientoConfiguracion { get; set; }
    }

    public class ConfiguracionCabeceraDTO
    {
        public int? IdAreaCapacitacion { get; set; }
        public int? IdTipoModalidad { get; set; }
        public int? IdPGeneral { get; set; }
        public int? IdPEspecifico { get; set; }
        public List<int>? ListaSeleccion { get; set; }
        public int? EnvioDiasAntes { get; set; }
        public string? TipoEnvio { get; set; }
        public bool? IsTodosWebinar { get; set; } = false;
        public int? DiasCalculoCuota { get; set; } = 0;
        public bool? IsPorPrograma { get; set; } = false;

        public string? TipoCuota { get; set; }
    }

    
    public class DatoLlamadaDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdIvrEjecucion { get; set; }
        public string CelularAlumno { get; set; }
        public string TextoEntrada { get; set; }
        public bool UsarMenu { get; set; }
        public string TextoMenu { get; set; }
        public string Anexo { get; set; }
        public int IntentoMaximo { get; set; }
        public int Intento { get; set; }
        public bool Concluido { get; set; }
        public bool Ejecutado { get; set; }
        public bool EjecutarIvr { get; set; }
    }

    public class DetalleIvrDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string TextoEntrada { get; set; }
        public bool UsarMenu { get; set; }
        public string TextoMenu { get; set; }
        public string Anexo { get; set; }
        public bool EjecutarIvr { get; set; } = false;
    }

}
