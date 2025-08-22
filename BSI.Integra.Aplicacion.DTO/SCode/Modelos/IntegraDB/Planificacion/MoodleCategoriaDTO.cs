using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class MoodleCategoriaDTO
    {
        public int? Id { get; set; }
        public int IdMoodleCategoriaTipo { get; set; }
        public string NombreCategoria { get; set; } = null!;
        public int IdCategoriaMoodle { get; set; }
    }
    public class MoodleCategoriaDetalle
    {
        public int Id { get; set; }
        public int IdCategoriaMoodle { get; set; }
        public string NombreCategoria { get; set; }
        public int? IdMoodleCategoriaTipo { get; set; }
        public string MoodleCategoriaTipo { get; set; }
        public bool AplicaProyecto { get; set; }
    }
}
