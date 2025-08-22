using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PgeneralConfiguracionPlantillaSubEstadoMatriculaDTO
    {
        public int? Id { get; set; }
        public int IdPgeneralConfiguracionPlantillaDetalle { get; set; }
        public int IdSubEstadoMatricula { get; set; }
    }
}
