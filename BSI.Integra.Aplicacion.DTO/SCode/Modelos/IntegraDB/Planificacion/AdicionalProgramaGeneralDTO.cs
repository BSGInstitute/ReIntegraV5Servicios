using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class AdicionalProgramaGeneralDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public string? Descripcion { get; set; }
        public string? NombreImagen { get; set; }
        public int? IdTitulo { get; set; }
        public string NombreTitulo { get; set; } = null!;
    }
}
