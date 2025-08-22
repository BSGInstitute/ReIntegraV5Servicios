using BSI.Integra.Aplicacion.DTO.SCode;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ActividadCabeceraDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaCreacion2 { get; set; }
        public int DuracionEstimada { get; set; }
        public bool ReproManual { get; set; }
        public bool ReproAutomatica { get; set; }
        public int Idplantilla { get; set; }
        public int IdActividadBase { get; set; }
        public DateTime? FechaModificacion2 { get; set; }
        public bool ValidaLlamada { get; set; }
        public int? IdPlantillaSpeech { get; set; }
        public int NumeroMaximoLlamadas { get; set; }
        public int? IdConjuntoLista { get; set; }
        public int? IdFrecuencia { get; set; }
        public DateTime? FechaInicioActividad { get; set; }
        public DateTime? FechaFinActividad { get; set; }
        public byte? DiaFrecuenciaMensual { get; set; }
        public bool? EsRepetitivo { get; set; }
        public TimeSpan? HoraInicio { get; set; }
        public TimeSpan? HoraFin { get; set; }
        public byte? CantidadIntevaloTiempo { get; set; }
        public int? IdTiempoIntervalo { get; set; }
        public bool? Activo { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public string PersonalAreaTrabajo { get; set; }
        public int? IdFacebookCuentaPublicitaria { get; set; }
        public string NombreActividadBase { get; set; }
        public string NombreConjuntoLista { get; set; }

    }

    public class ListaActividadDTO
    {
        public int Id { get; set; }
        public ActividadCabeceraCompuestoDTO? ActividadCabeceraLlamada { get; set; }
        public ActividadCabeceraIndividualDTO? ActividadCabeceraIndividual { get; set; }
        public ActividadCabeceraMasivoDTO? ActividadCabeceraMasivo { get; set; }

        public string ActividadBase { get; set; }
        public string Usuario { get; set; }
    }

    public class ActividadCabeceraCompuestoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion2 { get; set; }
        public int DuracionEstimada { get; set; }
        public bool ReproManual { get; set; }
        public bool ReproAutomatica { get; set; }
        public int Idplantilla { get; set; }
        public int IdActividadBase { get; set; }
        public DateTime FechaModificacion2 { get; set; }
        public bool ValidaLlamada { get; set; }
        public int? IdPlantillaSpeech { get; set; }
        public int NumeroMaximoLlamadas { get; set; }
        public List<ReprogramacionCabeceraDTO> Reprogramaciones { get; set; }
        public List<int> TipoDato { get; set; }
        public string Usuario { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public bool? EsEnvioMasivo { get; set; }
    }

    public class ActividadCabeceraIndividualDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdActividadBase { get; set; }
        public int IdPlantilla { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public bool? EsEnvioMasivo { get; set; }

    }

    public class ActividadCabeceraMasivoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdActividadBase { get; set; }
        public int? IdConjuntoLista { get; set; }
        public int? IdFrecuencia { get; set; }
        public DateTime? FechaInicioActividad { get; set; }
        public DateTime? FechaFinActividad { get; set; }
        public byte? DiaFrecuenciaMensual { get; set; }
        public bool? EsRepetitivo { get; set; }
        public TimeSpan? HoraInicio { get; set; }
        public TimeSpan? HoraFin { get; set; }
        public byte? CantidadIntevaloTiempo { get; set; }
        public int? IdTiempoIntervalo { get; set; }
        public bool? Activo { get; set; }
        public List<int> Semanal { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public bool? EsEnvioMasivo { get; set; }
    }
}
