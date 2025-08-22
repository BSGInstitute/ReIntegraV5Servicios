using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Expositor : BaseIntegraEntity
    {
        public int IdTipoDocumento { get; set; }
        [StringLength(50)]
        public string? NroDocumento { get; set; }
        [StringLength(300)]
        public string? PrimerNombre { get; set; }
        [StringLength(300)]
        public string? SegundoNombre { get; set; }
        [StringLength(300)]
        public string? ApellidoPaterno { get; set; }
        [StringLength(300)]
        public string? ApellidoMaterno { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? IdPaisProcedencia { get; set; }
        public int? IdCiudadProcedencia { get; set; }
        public int? IdReferidoPor { get; set; }
        [StringLength(50)]
        public string? TelfCelular1 { get; set; }
        [StringLength(50)]
        public string? TelfCelular2 { get; set; }
        [StringLength(50)]
        public string? TelfCelular3 { get; set; }
        [StringLength(100)]
        public string? Email1 { get; set; }
        [StringLength(100)]
        public string? Email2 { get; set; }
        [StringLength(100)]
        public string? Email3 { get; set; }
        [StringLength(300)]
        public string? Domicilio { get; set; }
        public int? IdPaisDomicilio { get; set; }
        public int? IdCiudadDomicilio { get; set; }
        [StringLength(300)]
        public string? LugarTrabajo { get; set; }
        public int? IdPaisLugarTrabajo { get; set; }
        public int? IdCiudadLugarTrabajo { get; set; }
        [StringLength(300)]
        public string? AsistenteNombre { get; set; }
        [StringLength(50)]
        public string? AsistenteTelefono { get; set; }
        [StringLength(50)]
        public string? AsistenteCelular { get; set; }
        public string? HojaVidaResumidaPerfil { get; set; }
        public string? HojaVidaResumidaSpeech { get; set; }
        public string? FormacionAcademica { get; set; }
        public string? ExperienciaProfesional { get; set; }
        public string? Publicaciones { get; set; }
        public string? PremiosDistinciones { get; set; }
        public string? OtraInformacion { get; set; }
        public bool? EsPersonaValida { get; set; }
        public int? IdPersonalAsignado { get; set; }
        [StringLength(150)]
        public string? FotoDocente { get; set; }
        [StringLength(150)]
        public string? UrlFotoDocente { get; set; }
    }
}
