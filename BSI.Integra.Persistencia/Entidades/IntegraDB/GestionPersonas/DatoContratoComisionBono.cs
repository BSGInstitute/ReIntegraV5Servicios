using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class DatoContratoComisionBono : BaseIntegraEntity
    {
        public int IdDatoContratoPersonal { get; set; }
        public decimal Monto { get; set; }
        public string Concepto { get; set; } = null!; 
        public string? TipoRemuneracionVariable { get; set; }
    }
}
