using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PersonalRemuneracion : BaseIntegraEntity
    {
        public int IdPersonal { get; set; }
        public int IdTipoPagoRemuneracion { get; set; }
        public int? IdEntidadFinanciera { get; set; }
        public string NumeroCuenta { get; set; }
        public bool Activo { get; set; }
    }
}
