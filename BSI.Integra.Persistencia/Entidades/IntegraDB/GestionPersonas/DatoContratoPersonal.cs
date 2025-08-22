using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class DatoContratoPersonal :BaseIntegraEntity
    {
        public int IdPersonal { get; set; }
        public int IdTipoContrato { get; set; }
        public bool EstadoContrato { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal RemuneracionFija { get; set; }
        public int? IdTipoPagoRemuneracion { get; set; }
        public int? IdEntidadFinancieraPago { get; set; }
        public string NumeroCuentaPago { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public int IdSedeTrabajo { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public int IdCargo { get; set; }
        public int? IdTipoPerfil { get; set; }
        public int? IdPersonalJefe { get; set; }
        public int? IdEntidadFinancieraCts { get; set; }
        public string NumeroCuentaCts { get; set; }
        public bool? EsPeridoPrueba { get; set; }
        public DateTime? FechaFinPeriodoPrueba { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdContratoEstado { get; set; }
        public string UrlDocumentoContrato { get; set; }

        
    }
}
