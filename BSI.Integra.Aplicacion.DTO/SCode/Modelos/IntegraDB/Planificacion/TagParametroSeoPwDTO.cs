using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class TagParametroSeoPwDTO
    {
        public int Id {  get; set; }
        public string Descripcion { get; set; } = null!;
        public int IdTagPw { get; set; }
        public int IdParametroSeopw { get; set; }
    }
    public class ParametroContenidoDTO
    {
        public int Id { get; set; }
        public int IdTag { get; set; }
        public int IdParametroSeo { get; set; }
        public string Nombre { get; set; }
        public string? Contenido { get; set; }
    }
}
