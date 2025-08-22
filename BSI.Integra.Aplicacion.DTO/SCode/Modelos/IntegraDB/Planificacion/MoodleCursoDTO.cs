using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class MoodleCursoDTO
    {
        public int? Id { get; set; }
        public int? IdCategoriaMoodle { get; set; }
        public int? IdCursoMoodle { get; set; }
        public string? NombreCategoria { get; set; }
        public string? NombreCursoMoodle { get; set; }

    }
}
