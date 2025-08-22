using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class OcurrenciaAlterno : BaseIntegraEntity
    {
        [StringLength(250)]
        public string Nombre { get; set; } = null!;
        [StringLength(250)]
        public string NombreM { get; set; } = null!;
        public int? NombreCs { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int? IdActividadCabecera { get; set; }
        public int? IdPlantillaSpeech { get; set; }
        public int IdEstadoOcurrencia { get; set; }
        public bool Oportunidad { get; set; }
        [StringLength(2)]
        public string RequiereLlamada { get; set; } = null!;
        [StringLength(100)]
        public string Roles { get; set; } = null!;
        [StringLength(20)]
        public string Color { get; set; } = null!;
        public int IdPersonalAreaTrabajo { get; set; }
        public int? IdTipoOcurrencia { get; set; }
    }
}