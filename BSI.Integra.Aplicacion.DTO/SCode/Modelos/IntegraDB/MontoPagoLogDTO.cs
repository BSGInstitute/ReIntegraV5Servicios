using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class MontoPagoLogDTO
    {
        public int IdMontoPagoLog { get; set; }
        public int IdPGeneral { get; set; }

        // PRECIO
        public decimal? PrecioOriginal { get; set; }
        public decimal? PrecioModificado { get; set; }

        // MONEDA
        public int? IdMonedaOriginal { get; set; }
        public string NombreMonedaOriginal { get; set; }
        public int? IdMonedaModificado { get; set; }
        public string NombreMonedaModificado { get; set; }

        // TIPO DE PAGO
        public int? IdTipoPagoOriginal { get; set; }
        public string NombreTipoPagoOriginal { get; set; }
        public int? IdTipoPagoModificado { get; set; }
        public string NombreTipoPagoModificado { get; set; }

        // PAÍS
        public int? IdPaisOriginal { get; set; }
        public string NombrePaisOriginal { get; set; }
        public int? IdPaisModificado { get; set; }
        public string NombrePaisModificado { get; set; }

        // DESCRIPCIÓN
        public string DescripcionOriginal { get; set; }
        public string DescripcionModificado { get; set; }

        // MATRÍCULA
        public string MatriculaOriginal { get; set; }
        public string MatriculaModificado { get; set; }

        // CUOTAS
        public string CuotasOriginal { get; set; }
        public string CuotasModificado { get; set; }

        // NRO CUOTAS
        public int? NroCuotasOriginal { get; set; }
        public int? NroCuotasModificado { get; set; }

        // VERSIÓN
        public int? PaqueteOriginal { get; set; }
        public string VersionOriginal { get; set; }
        public int? PaqueteModificado { get; set; }
        public string VersionModificado { get; set; }

        // LOG
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }

    public class FiltroMontoPagoHistoricoDTO
    {
        public DateTime? FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
        public int? IdPais { get; set; }
        public int? IdVersion{ get; set; }
        public int? IdTipoPago{ get; set; }
        public int IdPGeneral{ get; set; }
    }
}
