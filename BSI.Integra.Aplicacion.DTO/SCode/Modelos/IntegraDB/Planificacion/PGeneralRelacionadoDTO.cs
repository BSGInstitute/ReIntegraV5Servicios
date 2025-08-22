using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PGeneralRelacionadoDTO
    {
        public int? Id { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdPgeneralRelacionado { get; set; }
    }
    public class PGeneralProgramaRelacionadoDTO
    {
        public int Id { get; set; }
        public int IdRelacionado { get; set; }
        public string Nombre { get; set; }
    }
}
