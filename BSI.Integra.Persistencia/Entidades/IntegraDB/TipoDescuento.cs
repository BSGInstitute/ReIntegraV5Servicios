using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class TipoDescuento : BaseIntegraEntity
    {
        [StringLength(50)]
        public string Codigo { get; set; } = null!;
        [StringLength(100)]
        public string Descripcion { get; set; } = null!;
        public int Formula { get; set; }
        public int? PorcentajeGeneral { get; set; }
        public int? PorcentajeMatricula { get; set; }
        public int? FraccionesMatricula { get; set; }
        public int? PorcentajeCuotas { get; set; }
        public int? CuotasAdicionales { get; set; }
        public int? IdTipoDescuentoNivelAprobacion { get; set; }

        //para los tipos descuento
        public string Tipo { get; set; }
        public int? IdTipoDescuentoNivelAprobacion { get; set; }


        //Para TipoDescuentoAsesorCoordinadorPw
        public List<TipoDescuentoAsesorCoordinadorPw> TipoDescuentoAsesorCoordinadorPw { get; set; }
    }
}
