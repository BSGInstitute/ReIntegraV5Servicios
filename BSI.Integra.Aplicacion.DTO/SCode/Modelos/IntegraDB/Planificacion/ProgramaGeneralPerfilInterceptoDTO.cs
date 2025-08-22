using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ProgramaGeneralPerfilInterceptoDTO
    {
        public int IdPgeneral { get; set; }
        public double PerfilIntercepto { get; set; }
        public string PerfilEstado { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
