using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class AreaParametroSeoPwDTO
    {
        public int? Id { get; set; }
        public string Descripcion { get; set; }
        public int IdAreaCapacitacion { get; set; }
        public int IdParametroSeopw { get; set; }
    }
    public class AreaParametrosSeoPorIdAreaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int NumeroCaracteres { get; set; }
        public string Contenido { get; set; }
    }
}
