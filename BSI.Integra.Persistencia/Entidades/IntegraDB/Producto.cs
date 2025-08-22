using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Producto : BaseIntegraEntity
    {
        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        [StringLength(200)]
        public string Descripcion { get; set; } = null!;
        [StringLength(15)]
        public string CuentaGeneral { get; set; } = null!;
        [StringLength(15)]
        public string CuentaGeneralCodigo { get; set; } = null!;
        [StringLength(15)]
        public string CuentaEspecifica { get; set; } = null!;
        [StringLength(15)]
        public string CuentaEspecificaCodigo { get; set; } = null!;
        public int IdProductoPresentacion { get; set; }

    }
}
