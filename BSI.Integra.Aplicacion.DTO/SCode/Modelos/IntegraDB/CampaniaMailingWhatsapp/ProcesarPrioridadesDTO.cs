using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp
{
    public class ProcesarPrioridadesDTO
    {
        public List<PrioridadesDTO> ListaPrioridades { get; set; }
        public string Usuario { get; set; }
    }
    public class ProcesarPrioridadesCampaniaGeneralDetalleDTO
    {
        public int IdCampaniaGeneralDetalle { get; set; }
        public string Usuario { get; set; }
    }
    public class ValoresCalculadosEliminarTemporalDTO
    {
        public int CantidadMailing { get; set; }
        public int CantidadWhatsapp { get; set; }
    }
}
