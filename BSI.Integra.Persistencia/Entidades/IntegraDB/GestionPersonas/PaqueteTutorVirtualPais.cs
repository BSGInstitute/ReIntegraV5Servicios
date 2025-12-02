using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PaqueteTutorVirtualPais : BaseIntegraEntity
    {
        public int IdPaqueteTutorVirtual { get; set; }
        public int IdPais { get; set; }
        public int IdMoneda { get; set; }
        public decimal CostoIndividual { get; set; }
        public decimal CostoPaquete { get; set; }
        public List<PaqueteTutorVirtualBeneficio>? ListadoBeneficios { get; set; }
    }
}
