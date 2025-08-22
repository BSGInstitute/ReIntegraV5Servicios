using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class SubAreaParametroSeoPwDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public int IdSubAreaCapacitacion { get; set; }
        public int IdParametroSeoPw { get; set; }
    }
}
