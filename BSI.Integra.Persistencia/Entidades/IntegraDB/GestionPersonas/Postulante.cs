using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class Postulante : BaseIntegraEntity
    {
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        [StringLength(50)]
        public string ApellidoPaterno { get; set; } = null!;
        [StringLength(50)]
        public string? ApellidoMaterno { get; set; }
        [StringLength(50)]
        public string? NroDocumento { get; set; }
        [StringLength(50)]
        public string? Telefono { get; set; }
        [StringLength(50)]
        public string? Celular { get; set; }
        [StringLength(100)]
        public string? Email { get; set; }
        [StringLength(50)]
        public string? Telefono2 { get; set; }
        [StringLength(50)]
        public string? Celular2 { get; set; }
        [StringLength(50)]
        public string? Celular3 { get; set; }
        [StringLength(100)]
        public string? Email2 { get; set; }
        [StringLength(100)]
        public string? Email3 { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdTipoDocumento { get; set; }
        public int? IdSexo { get; set; }
        public string? UrlPerfilFacebook { get; set; }
        public string? UrlPerfilLinkedin { get; set; }
        public bool? EsProcesoAnterior { get; set; }
        public int? Edad { get; set; }
        public bool? TieneHijo { get; set; }
        public int? CantidadHijo { get; set; }
        public int? IdConvocatoriaPersonal { get; set; }
        public int? IdPersonalOperadorProceso { get; set; }
        public int? IdPaginaReclutadoraPersonal { get; set; }
        public int? IdPostulanteNivelPotencial { get; set; }
    }
}
