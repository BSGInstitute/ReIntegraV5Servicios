using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class InteraccionPagina : BaseIntegraEntity
    {
        public int? IdAlumno { get; set; }
        public int? IdInteraccionScore { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPespecifico { get; set; }
        public int? IdPgeneralGenerico { get; set; }
        public int? IdSubAreaCapacitacion { get; set; }
        public int? IdAreaCapacitacion { get; set; }
        public int? IdPgeneralGenericoSiguiente { get; set; }
        public int? IdSubAreaCapcitacionSiguiente { get; set; }
        public int? IdAreaCapacitacionSiguiente { get; set; }
        public int? IdPgeneralGenericoAnterior { get; set; }
        public int? IdSubAreaCapcitacionAnterior { get; set; }
        public int? IdAreaCapacitacionAnterior { get; set; }
        public int? IdCategoriaInteraccion { get; set; }
        public int? IdCategoriaInteraccionSiguiente { get; set; }
        public int? IdCategoriaInteraccionAnterior { get; set; }
        public int? IdSubcategoriaInteraccion { get; set; }
        public int? IdSubCategoriaInteraccionSiguiente { get; set; }
        public int? IdTipoInteraccion { get; set; }
        public Guid? IpIdCookie { get; set; }
        public int? IpScore { get; set; }
        public int? IpValorMedible { get; set; }
        [StringLength(16)]
        public string? IpIp { get; set; }
        public DateTime? IpFechaInicio { get; set; }
        public DateTime? IpFechaFin { get; set; }
        [StringLength(2000)]
        public string? UrlActual { get; set; }
        [StringLength(2000)]
        public string? UrlAnterior { get; set; }
        [StringLength(2000)]
        public string? UrlSiguiente { get; set; }
        [StringLength(150)]
        public string? Correo { get; set; }
        [StringLength(50)]
        public string? Ip { get; set; }
        public int? IdProveedorCampaniaIntegra { get; set; }
        [StringLength(50)]
        public string? IdConjuntoAnuncio { get; set; }
        public int? IdCategoriaOrigen { get; set; }
    }
}
