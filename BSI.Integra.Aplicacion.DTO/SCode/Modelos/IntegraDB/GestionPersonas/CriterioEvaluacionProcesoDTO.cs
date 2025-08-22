using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class CriterioEvaluacionProcesoDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
    }
    public class CriterioEvaluacionProcesoExamenDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public bool Relacionado { get; set; }
    }
}
