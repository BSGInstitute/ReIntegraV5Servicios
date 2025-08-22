using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp
{
    public class ConjuntoListaResultadoDTO
    {
  
        public int Id { get; set; }
 
        public int IdAlumno { get; set; }
  
        public int IdConjuntoListaDetalle { get; set; }
   
        public bool? EsVentaCruzada { get; set; }

        public int NroEjecucion { get; set; }

        public bool Activo { get; set; }

        public int? IdOportunidad { get; set; }


    }
}
