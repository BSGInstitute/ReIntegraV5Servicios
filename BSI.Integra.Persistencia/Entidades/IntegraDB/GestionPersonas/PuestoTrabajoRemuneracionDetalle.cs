using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PuestoTrabajoRemuneracionDetalle : BaseIntegraEntity
    {
        public int Id { get; set; }
        public int IdPuestoTrabajoRemuneracion { get; set; }
        public int IdRemuneracionTipo { get; set; } // IdRemuneracion
        public int IdRemuneracionTipoCobro { get; set; } // IdClaseRemuneracion
        public int IdRemuneracionFormaCobro { get; set; } 
        public int IdRemuneracionPeriodoCobro { get; set; } 
        public bool EsTasa { get; set; }
        public decimal? MontoFijo { get; set; }
        public int? IdMonedaMontoFijo { get; set; } // IdMoneda
        public decimal? PorcentajeTasa { get; set; }
        public string DescripcionEquipo { get; set; }
        public bool TieneCondicion { get; set; }
        public int? IdRemuneracionDescripcionMonetaria { get; set; } 
        public decimal? RangoValorMinimo { get; set; }
        public decimal? RangoValorMaximo { get; set; }
        public int? IdMonedaRangoValor { get; set; } // IdMonedaValorVariable
        public decimal? IngresoMensual { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public int? IdMigracion { get; set; }
        public virtual TPuestoTrabajoRemuneracion IdPuestoTrabajoRemuneracionNavigation { get; set; } = null!;
    }
}
