using BSI.Integra.Aplicacion.DTO.SCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class SubNivelCcDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public int IdAreaCc { get; set; }
    }
    public class SubNivelCcListaDTO
    {
        public int Total { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int IdAreaCC { get; set; }
    }
}
