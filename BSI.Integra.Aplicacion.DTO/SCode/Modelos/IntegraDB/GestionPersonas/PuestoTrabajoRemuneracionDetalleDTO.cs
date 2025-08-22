using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas
{
    public class PuestoTrabajoRemuneracionDetalleDTO
    {
        public int Id { get; set; }
        public int? IdPuestoTrabajoRemuneracion { get; set; }
        public int? IdRemuneracion { get; set; }
        public int? IdTipoRemuneracion { get; set; }
        public int? IdClaseRemuneracion { get; set; }
        public int? IdRemuneracionFormaCobro { get; set; }
        public int? IdPeriodoRemuneracion { get; set; }
        public bool? Tasa { get; set; }
        public decimal? Monto { get; set; }
        public int? IdMoneda { get; set; }
        public decimal? PorcentajeTasa { get; set; }
        public string DescripcionEquipo { get; set; }
        public bool? TieneCondicion { get; set; }
        public int? IdDescripcionMonetaria { get; set; }
        public decimal? ValorMinimo { get; set; }
        public decimal? ValorMaximo { get; set; }
        public int? IdMonedaValorVariable { get; set; }
        public decimal? IngresoMensual { get; set; }
        public bool? Estado { get; set; }
        public string? UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; } = null!;
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }

}
