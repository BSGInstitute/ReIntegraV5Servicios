using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Log : BaseIntegraEntity
    {
        [StringLength(15)]
        public string Ip { get; set; } = null!;
        [StringLength(255)]
        public string Usuario { get; set; } = null!;
        [StringLength(50)]
        public string Maquina { get; set; } = null!;
        [StringLength(512)]
        public string Ruta { get; set; } = null!;
        [StringLength(512)]
        public string Parametros { get; set; } = null!;
        [StringLength(4000)]
        public string Mensaje { get; set; } = null!;
        [StringLength(2500)]
        public string? Excepcion { get; set; }
        [StringLength(50)]
        public string Tipo { get; set; } = null!;
        public int? IdPadre { get; set; }
    }
}
