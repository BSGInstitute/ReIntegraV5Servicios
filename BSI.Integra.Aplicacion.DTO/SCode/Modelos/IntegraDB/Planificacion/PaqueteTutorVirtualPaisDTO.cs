using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PaqueteTutorVirtualPaisDTO
    {
        public int Id { get; set; }
        public int IdPaqueteTutorVirtual { get; set; }
        public int IdPais { get; set; }
        public int IdMoneda { get; set; }
        public decimal CostoIndividual { get; set; }
        public decimal CostoPaquete { get; set; }
        public string? NombrePaquete { get; set; }
        public string? NombrePais { get; set; }
        public string? CodigoMoneda { get; set; }
    }
}
