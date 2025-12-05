using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class MontoPagoLog : BaseIntegraEntity
    {
        public int IdMontoPago { get; set; }
        public decimal PrecioOriginal { get; set; }
        public decimal? PrecioModificado { get; set; }
        public string PrecioLetrasOriginal { get; set; } = null!;
        public string? PrecioLetrasModificado { get; set; }
        public int IdMonedaOriginal { get; set; }
        public int? IdMonedaModificado { get; set; }
        public decimal? MatriculaOriginal { get; set; }
        public decimal? MatriculaModificado { get; set; }
        public decimal? CuotasOriginal { get; set; }
        public decimal? CuotasModificado { get; set; }
        public int? NroCuotasOriginal { get; set; }
        public int? NroCuotasModificado { get; set; }
        public int? IdTipoDescuentoOriginal { get; set; }
        public int? IdTipoDescuentoModificado { get; set; }
        public int? IdPgeneralOriginal { get; set; }
        public int? IdPgeneralModificado { get; set; }
        public int? IdTipoPagoOriginal { get; set; }
        public int? IdTipoPagoModificado { get; set; }
        public int? IdPaisOriginal { get; set; }
        public int? IdPaisModificado { get; set; }
        public string? VencimientoOriginal { get; set; }
        public string? VencimientoModificado { get; set; }
        public string? PrimeraCuotaOriginal { get; set; }
        public string? PrimeraCuotaModificado { get; set; }
        public bool? CuotaDobleOriginal { get; set; }
        public bool? CuotaDobleModificado { get; set; }
        public string? DescripcionOriginal { get; set; }
        public string? DescripcionModificado { get; set; }
        public bool? VisibleWebOriginal { get; set; }
        public bool? VisibleWebModificado { get; set; }
        public int? PaqueteOriginal { get; set; }
        public int? PaqueteModificado { get; set; }
        public bool? PorDefectoOriginal { get; set; }
        public bool? PorDefectoModificado { get; set; }
        public decimal? MontoDescontadoOriginal { get; set; }
        public decimal? MontoDescontadoModificado { get; set; }
        public string? Accion { get; set; }
    }
}
