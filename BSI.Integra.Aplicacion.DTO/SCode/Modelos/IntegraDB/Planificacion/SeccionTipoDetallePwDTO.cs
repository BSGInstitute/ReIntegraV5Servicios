using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class SeccionTipoDetallePwDTO
    {
        public int? Id { get; set; }
        public int IdSeccionPw { get; set; }
        public string NombreTitulo { get; set; } = null!;
        public int? IdSeccionTipoContenido { get; set; }
    }
    public class SeccionTipoDetallePwEstructuraProgramaDTO
    {
        public string NombreTitulo { get; set; }
        public int IdSeccionTipoDetallePw { get; set; }
    }
}
