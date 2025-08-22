using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class EstructuraEspecificaTareaDTO
    {
        public int IdEstructuraEspecifica { get; set; }
        public int IdTarea { get; set; }
        public string NombreTarea { get; set; }
        public int OrdenCapitulo { get; set; }
        public int IdDocumentoSeccionPw { get; set; }
    }
}
