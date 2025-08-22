using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ConfigurarVideoPrograma : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        public int IdDocumentoSeccionPw { get; set; }
        [StringLength(150)]
        public string VideoId { get; set; } = null!;
        [StringLength(100)]
        public string? TotalMinutos { get; set; }
        [StringLength(350)]
        public string? Archivo { get; set; }
        [StringLength(150)]
        public string? NroDiapositivas { get; set; }
        public bool Configurado { get; set; }
        public Guid? IdMigracion { get; set; }
        public bool? ConImagenVideo { get; set; }
        [StringLength(100)]
        public string? ImagenVideoNombre { get; set; }
        [StringLength(50)]
        public string? ImagenVideoAncho { get; set; }
        [StringLength(50)]
        public string? ImagenVideoAlto { get; set; }
        public bool? ConImagenDiapositiva { get; set; }
        [StringLength(100)]
        public string? ImagenDiapositivaNombre { get; set; }
        [StringLength(50)]
        public string? ImagenDiapositivaAncho { get; set; }
        [StringLength(50)]
        public string? ImagenDiapositivaAlto { get; set; }
        public int? NumeroFila { get; set; }
        [StringLength(4000)]
        public string? Token { get; set; }
        public int? ImagenVideoPosicionX { get; set; }
        public int? ImagenVideoPosicionY { get; set; }
        public int? ImagenDiapositivaPosicionX { get; set; }
        public int? ImagenDiapositivaPosicionY { get; set; }
        [StringLength(150)]
        public string? VideoIdBrightcove { get; set; }
        public bool? Activo { get; set; }
        public List<SesionConfigurarVideo> SesionConfigurarVideos { get; set; }
    }
}
