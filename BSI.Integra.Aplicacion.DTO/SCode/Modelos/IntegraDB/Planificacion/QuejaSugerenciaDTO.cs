using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class QuejaSugerenciaDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoQueja { get; set; }
        public string Descripcion { get; set; }
        public string Alumno { get; set; }
        public string ProgramaGeneral { get; set; }
        public string ProgramaEspecifico { get; set; }
        public string CodigoMatricula { get; set; }
        public string Correo { get; set; }
        public string AsistenteAAC { get; set; }
        public string CentroCosto { get; set; }
        public string Docente { get; set; }
    }

    public class QuejaSugerenciaFiltroDTO
    {
        public List<int>? Area { get; set; }
        public List<int>? SubArea { get; set; }
        public List<int>? ProgramaGeneral { get; set; }
        public List<int>? Tipo { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
