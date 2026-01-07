using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.Configuracion
{
    public class CategoriaArgumento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }

    public class CrearEditarCategoriaArgumentoDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
    }
}
